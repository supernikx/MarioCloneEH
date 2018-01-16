using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            other.gameObject.GetComponent<Player>().startposition = transform.position;
            gameObject.SetActive(false);
        }
    }
}
