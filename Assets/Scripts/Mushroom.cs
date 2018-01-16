using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            Player p = other.gameObject.GetComponent<Player>();
            gameObject.SetActive(false);
            p.isBig = true;
            p.changeSize = true;
            p.score += 100;
        }
    }
}
