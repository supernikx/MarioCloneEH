using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Player p = collision.gameObject.GetComponent<Player>();
            p.coins++;
            gameObject.SetActive(false);
            if (p.coins >= 10)
            {
                p.coins -= 10;
                p.life++;
            }
        }
    }*/

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Player p = collision.gameObject.GetComponent<Player>();
            p.coins++;
            gameObject.SetActive(false);
            if (p.coins >= 10)
            {
                p.coins -= 10;
                p.life++;
            }
        }

    }
}
