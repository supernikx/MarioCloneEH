using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUi : MonoBehaviour {

    public Text lives;
    public Player player;
    private void Start()
    {
        lives.text = player.life.ToString();
    }

    void Update () {
        lives.text = player.life.ToString();
    }
}
