using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    /// <summary>
    /// Nome del personaggio
    /// </summary>
    public string name;
    public int life;
    protected int startlife;
    public bool isAlive;
    /// <summary>
    /// velocità di movimento (maggiore valore, maggiore è la velocità di movimento)
    /// </summary>
    public float movementSpeed;
    protected bool isJump;
    /// <summary>
    /// forza con cui spinge l'oggetto verso l'alto
    /// </summary>
    public float jumpforce;

    protected Rigidbody rb;
    protected Transform tr;
    [HideInInspector]
    public Vector3 startposition;
    protected Renderer render;
    protected Collider collider;
    protected bool canControll;

    private void Awake()
    {
        isAlive = true;
        startlife = life;
        tr = GetComponent<Transform>();
        startposition = tr.position;
        render = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }

    /// <summary>
    /// Aggiunge una forzaa verso l'alto in base alla jumpforce
    /// </summary>
    public void Jump()
    {
        //rb.AddForce(0, jumpforce * Time.deltaTime, 0); //senza vettore normalizzato
        //azzera la velocity
        rb.velocity = new Vector3(0, 0, 0);
        rb.AddForce(Vector3.up * jumpforce);//con vettore normalizzato
    }

    /// <summary>
    /// Toglie un danno a questo oggetto
    /// </summary>
    public void TakeDamage(int damageTaken)
    {
        life -= damageTaken;
        if (life <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// il Render dell'oggetto viene diabilitato, se è un player respawn dopo il tempo settato nel deathTimer
    /// </summary>
    /// virtual serve per poter riscrivere la funzione
    public virtual void Die()
    {
        isAlive = false;
        if (render != null)
        {
            render.enabled = false;
        }

    }
}
