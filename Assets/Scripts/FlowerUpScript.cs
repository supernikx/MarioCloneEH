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
            if (p.status == PlayerStatus.big)
            {
                p.status = PlayerStatus.flower;
                StartCoroutine(DisablePowerUp());
                rend.enabled = false;
            }
        }
    }

    private IEnumerator DisablePowerUp()
    {
        yield return new WaitForSeconds(seconds: powerUpDuration);
        p.status = PlayerStatus.big;
        gameObject.SetActive(false);
    }
}
