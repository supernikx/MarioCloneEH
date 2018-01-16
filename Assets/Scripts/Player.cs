using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Unit
{
    //variabili per il settaggio del player
    [HideInInspector]
    public bool isBig, changeSize;
    public Vector3 little,big;
    int doubleJump = 0;
    public int deathTimer=2;
    [HideInInspector]
    public int score,coins;
    public GameManager gameManger;
    // Use this for initialization
    void Start()
    { 
        isBig = false;
        tr.localScale = little;
        canControll = true;
        isJump = false;           
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.freezeRotation = true;
        }
        if (name == "Luigi" && SaveData.instance.player2spawned==false)
        {
            DestroyImmediate(gameObject);
            return;
        }
        if (SaveData.instance.mario_datastored)
        {
            Load();
        }
        ChangeSize();
    }

    // Update is called once per frame
    void Update()
    {
        if (canControll)
        {
            Controll();
        }

        if (changeSize)
        {
            ChangeSize();
        }
    }

    //carica i dati dall'global object
    public void Load()
    {
        if (name == "Mario")
        {
            life = SaveData.instance.mario_life;
            score = SaveData.instance.mario_score;
            isBig = SaveData.instance.mario_isBig;
            coins = SaveData.instance.mario_coins;
        }
        else if (name == "Luigi")
        {
                life = SaveData.instance.luigi_life;
                score = SaveData.instance.luigi_score;
                isBig = SaveData.instance.luigi_isBig;
                coins = SaveData.instance.luigi_coins;
        }
    }

    //salva i dati all'interno del global object
    public void Save()
    {
        if (name == "Mario")
        {
            SaveData.instance.mario_life = life;
            SaveData.instance.mario_isBig = isBig;
            SaveData.instance.mario_score = score;
            SaveData.instance.mario_coins = coins;
            SaveData.instance.mario_datastored = true;
            SaveData.instance.levelreached = SceneManager.GetActiveScene().buildIndex+1;
        }else if (name == "Luigi")
        {
            SaveData.instance.luigi_life = life;
            SaveData.instance.luigi_isBig = isBig;
            SaveData.instance.luigi_score = score;
            SaveData.instance.luigi_coins = coins;
            SaveData.instance.luigi_datastored = true;
        }
    }

    //cambiare le dimensioni del player tra grande e piccolo
    private void ChangeSize()
    {
        changeSize = false;
        if (isBig)
        {
            tr.localScale = big;
        }
        else
        {
            tr.localScale = little;
        }
    }

    //legge gli imput e muove il player
    private void Controll()
    {
        if (name == "Mario")
        {
            //mettere in pausa il gioco
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                   gameManger.Pause();
            }
            //Salto Mario
            if (Input.GetKeyDown(KeyCode.Space) && !isJump)
            {
                doubleJump++;
                Jump();
                DoubleJump();
            }
            //Movimento sinistra mario
            else if (Input.GetKey(KeyCode.A))
            {
                tr.position += new Vector3(movementSpeed * Time.deltaTime, 0, 0);
            }
            //Movimento destra mario
            else if (Input.GetKey(KeyCode.D))
            {
                tr.position += new Vector3(-movementSpeed * Time.deltaTime, 0, 0);
            }
        }
        else if (name == "Luigi")
        {
            //salto Luigi
            if (Input.GetKeyDown(KeyCode.UpArrow) && !isJump)
            {
                doubleJump++;
                Jump();
                DoubleJump();
            }
            //Movimento sinistra luigi
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                tr.position += new Vector3(movementSpeed * Time.deltaTime, 0, 0);
            }
            //Movimento destra luigi
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                tr.position += new Vector3(-movementSpeed * Time.deltaTime, 0, 0);
            }
        }
    }

    //controlla se il player può eseguire un doppio salto
    private void DoubleJump()
    {
        if (doubleJump == 2)
        {
            doubleJump = 0;
            isJump = true;
        }
    }

    //gestisce le collisioni del player
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            //permette di saltare ancora
            case "block":
            case "platform":
                if (collision.contacts[0].normal.y == 1.0)
                {
                    isJump = false;
                }
                break;
            
            //respawna il player che è caduto dalla mappa
            case "Respawn":
                Die();
                break;
        }

    }

    //quando diventa invisibile respawna
    private void OnBecameInvisible()
    {
        canControll = false;
        tr.position = startposition;
        life--;
        if (life <= 0)
        {
            gameManger.EndGame();
        }
        else
        {
            isBig = false;
            changeSize = true;
            StartCoroutine(Respawn(deathTimer));
        }
    }

    //gestisce il respawn del player, riportandolo al punto iniziale
    //si può usasre anche Invoke(funzione,tempo di attesa);
    protected IEnumerator Respawn(int deathTimer)
    {
        yield return new WaitForSeconds(seconds: deathTimer);
        isAlive = true;
        render.enabled = true;
        canControll = true;
    }

}
