using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Unit
{
    //variabili per il settaggio del player

    private bool isRight;
    public GameObject fireball;

    [HideInInspector]
    public bool changeSize;
    public PlayerStatus status;
    public Vector3 small,big;
    int doubleJump = 0;
    public int deathTimer=2;
    [HideInInspector]
    public int score,coins;
    public GameManager gameManger;
    // Use this for initialization
    void Start()
    { 
        status = PlayerStatus.small;
        tr.localScale = small;
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
            status = SaveData.instance.mario_status;
            coins = SaveData.instance.mario_coins;
        }
        else if (name == "Luigi")
        {
                life = SaveData.instance.luigi_life;
                score = SaveData.instance.luigi_score;
                status = SaveData.instance.luigi_status;
                coins = SaveData.instance.luigi_coins;
        }
    }

    //salva i dati all'interno del global object
    public void Save()
    {
        if (name == "Mario")
        {
            SaveData.instance.mario_life = life;
            SaveData.instance.mario_status = status;
            SaveData.instance.mario_score = score;
            SaveData.instance.mario_coins = coins;
            SaveData.instance.mario_datastored = true;
            SaveData.instance.levelreached = SceneManager.GetActiveScene().buildIndex+1;
        }else if (name == "Luigi")
        {
            SaveData.instance.luigi_life = life;
            SaveData.instance.luigi_status = status;
            SaveData.instance.luigi_score = score;
            SaveData.instance.luigi_coins = coins;
            SaveData.instance.luigi_datastored = true;
        }
    }
    //cambiare le dimensioni del player tra grande e piccolo
    private void ChangeSize()
    {
        changeSize = false;
        if (status == PlayerStatus.big)
        {
            tr.localScale = big;
        }
        else if (status == PlayerStatus.small)
        {
            tr.localScale = small;
        }
        else if (status == PlayerStatus.flower)
        {
            tr.localScale = big;
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
                isRight = false;
            }
            //Movimento destra mario
            else if (Input.GetKey(KeyCode.D))
            {
                tr.position += new Vector3(-movementSpeed * Time.deltaTime, 0, 0);
                isRight = true;
            }

            if(Input.GetKeyDown(KeyCode.F) && status==PlayerStatus.flower)
            {
                if (isRight)
                {
                    FireBallScript ball=Instantiate(fireball, tr.position + new Vector3(-1, 0, 0), Quaternion.identity).GetComponent<FireBallScript>();
                    ball.Shoot(isRight);
                }
                else
                {
                    FireBallScript ball=Instantiate(fireball, tr.position + new Vector3(1, 0, 0), Quaternion.identity).GetComponent<FireBallScript>();
                    ball.Shoot(isRight);
                }
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
            status = PlayerStatus.small;
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

public enum PlayerStatus
{
    small,big,flower
}
