using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour {
    private Rigidbody rb;
    private int bounces;
    private bool right;
    public float xforce, yforce;

    public void Start()
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
    private void OnCollisionEnter(Collision collision)
    {
        bounces++;
        if (right)
        {
            if (collision.gameObject.tag == "platform")
            {
                rb.velocity = new Vector3(0, 0, 0);
                rb.AddForce(-1 * xforce, 1 * yforce, 0);
            }
            else
            {
                bounces = 5;
            }

            if (bounces > 4)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (collision.gameObject.tag == "platform")
            {
                rb.velocity = new Vector3(0, 0, 0);
                rb.AddForce(1 * xforce, 1 * yforce, 0);
            }
            else
            {
                bounces = 5;
            }

            if (bounces > 4)
            {
                Destroy(gameObject);
            }
        }
    }
}
