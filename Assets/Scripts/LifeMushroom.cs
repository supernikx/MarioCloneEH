using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeMushroom : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            Player p = other.gameObject.GetComponent<Player>();
            gameObject.SetActive(false);
            p.life++;
            p.score += 100;
        }
    }
}
