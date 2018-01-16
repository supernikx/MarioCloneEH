using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour {

    public Text coins;
    public Player player;
    private void Start()
    {
        coins.text = player.coins.ToString();
    }

    void Update()
    {
        coins.text = player.coins.ToString();
    }
}
