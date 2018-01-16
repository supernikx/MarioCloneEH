using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour {
    public Object nextLevel;
    private Player[] p;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player")
        {
            p = FindObjectsOfType<Player>();
            for (int i=0; i< p.Length; i++)
            {
                p[i].Save();
            }
            if (nextLevel == null)
            {
                print("Hai vinto");
            }
            else
            {
                SceneManager.LoadScene(nextLevel.name);
            }
        }
    }
}
