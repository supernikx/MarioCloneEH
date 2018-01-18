using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerUpScript : MonoBehaviour {
    public int powerUpDuration;
    private Player p;
    private Renderer rend;
    private void Start()
    {
        rend = GetComponent<Renderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "player")
        {
            p = other.gameObject.GetComponent<Player>();
            p.isFlower = true;
            StartCoroutine(DisablePowerUp());
            rend.enabled = false;
        }
    }

    private IEnumerator DisablePowerUp()
    {
        yield return new WaitForSeconds(seconds: powerUpDuration);
        p.isFlower = false;
        gameObject.SetActive(false);
    }
}
