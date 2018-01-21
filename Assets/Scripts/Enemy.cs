using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit {
    
    [Header("Movement")]
    //variabili per il movimento dei nemici
    public bool canMove;
    public bool moveOrizzontal;
    public bool moveVertical;
    public Vector3 arrivePoint;
    private bool hasArrived, right, up;

    [Header("Other settings")]
    //variabili per il settaggio dei nemici
    public bool canDie;
    public int damage, points;   
    
    Vector3 tempPosition;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        tempPosition = arrivePoint;
        
        if (canMove)
        {
            //controlla se il nemico si deve muovere in orrizontale e inizializza le variabili di conseguenza
            if (moveOrizzontal)
            {
                moveVertical = false;
                //controlla se il nemico si trova a destra o a sinistra del punto inserito nell'arrive point
                if (tr.position.x > tempPosition.x)
                {
                    hasArrived = false;
                    right = true;
                }
                else
                {
                    hasArrived = true;
                    right = false;
                }
            }
            //controlla se il nemico si deve muovere in verticale e inizializza le variabili di conseguenza
            else if (moveVertical)
            {
                moveOrizzontal = false;
                //controlla se il nemico si trova a sopra o sotto del punto inserito nell'arrive point
                if (tr.position.y < tempPosition.y)
                {
                    hasArrived = false;
                    up = true;
                }
                else
                {
                    hasArrived = true;
                    up = false;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (isAlive)
        {
            Movement();
        }
    }

    //fa muovere il nemico avanti e indietro dal punto di partenza fino all'arrivePoint
    public void Movement ()
    {
        
        if (canMove)
        {
            if (moveOrizzontal)
            {
                //in caso il nemico si trovi alla sinistra del punto di arrivo
                if (right)
                {
                    //sposta il nemico verso il punto di arrivo (variabile arrivePoint)
                    if (tr.position.x > tempPosition.x)
                    {
                        if (hasArrived)
                        {
                            tempPosition = arrivePoint;
                            hasArrived = false;
                        }
                        tr.Translate(Vector3.left * Time.deltaTime * movementSpeed);
                    }
                    //sposta il nemico verso il punto di partenza
                    else
                    {
                        if (!hasArrived)
                        {
                            tempPosition = startposition;
                            hasArrived = true;
                        }
                        transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
                    }
                }

                //in caso il nemico si trovi alla destra del punto di arrivo
                if (!right)
                {
                    //sposta il nemico verso il punto di partenza
                    if (tr.position.x > tempPosition.x)
                    {
                        if (hasArrived)
                        {
                            tempPosition = startposition;
                            hasArrived = false;
                        }
                        tr.Translate(Vector3.left * Time.deltaTime * movementSpeed);
                    }
                    //sposta il nemico verso il punto di arrivo (variabile arrivePoint)
                    else
                    {
                        if (!hasArrived)
                        {
                            tempPosition = arrivePoint;
                            hasArrived = true;
                        }
                        transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
                    }
                }
            }
            if (moveVertical)
            {
                //in caso il nemico si trova sotto il punto di arrivo
                if (up)
                {
                    //sposta il nemico verso il punto di arrivo (variabile arrivePoint)
                    if (tr.position.y < tempPosition.y)
                    {
                        if (hasArrived)
                        {
                            tempPosition = arrivePoint;
                            hasArrived = false;
                        }
                        tr.Translate(Vector3.up * Time.deltaTime * movementSpeed);
                    }
                    //sposta il nemico verso il punto di partenza
                    else
                    {
                        if (!hasArrived)
                        {
                            tempPosition = startposition;
                            hasArrived = true;
                        }
                        transform.Translate(Vector3.down * Time.deltaTime * movementSpeed);
                    }
                }

                //in caso il nemico si trova sotto il punto di arrivo
                if (!up)
                {
                    //sposta il nemico verso il punto di partenza
                    if (tr.position.y < tempPosition.y)
                    {
                        if (hasArrived)
                        {
                            tempPosition = startposition;
                            hasArrived = false;
                        }
                        tr.Translate(Vector3.up * Time.deltaTime * movementSpeed);
                    }
                    //sposta il nemico verso il punto di arrivo (variabile arrivePoint)
                    else
                    {
                        if (!hasArrived)
                        {
                            tempPosition = arrivePoint;
                            hasArrived = true;
                        }
                        transform.Translate(Vector3.down * Time.deltaTime * movementSpeed);
                    }
                }
            }
        }
    }

    //riscrive la funzione Die presente in Unit disattivando anche il collider
    public override void Die()
    {
        base.Die();
        if (collider != null)
            collider.enabled = false;
    }

    //controlla le collisioni del nemico
    private void OnCollisionEnter(Collision collision)
    {
        //quando il player colpisce un nemico, se lo colpisce in testa lo uccide, se lo colpisce negli altri punti riceve 1 danno
        if (collision.gameObject.tag == "player")
        {
            Player p = collision.gameObject.GetComponent<Player>();
            //controlla se il meico può morire (variabile canDie)
            if (canDie)
            {
                //se il nemico viene colpito in testa muore
                if (collision.contacts[0].normal.y <= -0.7)
                {
                    Die();
                    p.score += points;
                    p.Jump();
                }
                //se il nemico colpisce il player ed è grande allora diventa piccolo
                else if (p.status==PlayerStatus.big)
                {
                    p.status = PlayerStatus.small;
                    p.changeSize = true;
                }
                //se il nemico colpisce il player ed è fiore diventa grande
                else if (p.status == PlayerStatus.flower)
                {
                    p.status = PlayerStatus.big;
                    p.changeSize = true;
                }
                //se il nemico colpisce il player ed è piccoo muore
                else
                {
                    p.Die();
                }
            }
            //se il player colpisce un nemico che non può morire, il player muore
            else
            {
                p.Die();
            }
        }
    }
}
