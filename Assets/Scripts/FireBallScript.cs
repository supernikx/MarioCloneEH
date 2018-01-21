using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour {
    private Rigidbody rb;
    private int bounces;
    private bool right;
    public float xforce, yforce;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }	

    // Use this for initialization
    public void Shoot(bool right)
    {
        this.right = right;
        if (right)
        {   
            rb.AddForce(-1 * 700, 1 * 300, 0);
        }
        else
        {
            rb.AddForce(1 * 700, 1 * 300, 0);
        }
    }

    private void DestroyBall()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        bounces++;
        switch (collision.gameObject.tag)
        {
            case "platform":
                if (right)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                    rb.AddForce(-1 * xforce, 1 * yforce, 0);
                }
                else
                {
                    rb.velocity = new Vector3(0, 0, 0);
                    rb.AddForce(1 * xforce, 1 * yforce, 0);
                }
                break;
            case "enemy":
                Enemy e = collision.gameObject.GetComponent<Enemy>();
                if (e.canDie)
                {
                    e.Die();
                }
                DestroyBall();
                break;
            default:
                bounces = 5;
                break;
        }

        if (bounces > 4)
        {
            DestroyBall();
        }
    }
}
