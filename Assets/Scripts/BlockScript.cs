using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {
    public Material[] material;
    Renderer rend;
    public GameObject powerUp;
    public int points;
    private Transform tr;
    private bool oneTime;
    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.sharedMaterial = material[0];
        oneTime = true;
        tr = gameObject.GetComponent<Transform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Player p = collision.gameObject.GetComponent<Player>();
            if (collision.contacts[0].normal.y == 1 && powerUp == null)
            {
                gameObject.SetActive(false);
                p.score += points;
            }
            else if (collision.contacts[0].normal.y == 1 && oneTime && powerUp != null)
            {
                rend.sharedMaterial = material[1];
                Instantiate(powerUp, tr.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
                p.score += points;
                oneTime = false;
            }
            else if (collision.contacts[0].normal.y == 1 && !oneTime && p.status==PlayerStatus.big)
            {
                gameObject.SetActive(false);
                p.score += points;
            }
        }
    }
}
