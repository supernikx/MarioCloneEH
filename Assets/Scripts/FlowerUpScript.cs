using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerUpScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "player")
        {
            Player p = other.gameObject.GetComponent<Player>();
            p.isFlower = true;
            gameObject.SetActive(false);

            
        }
    }

}
