using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUi : MonoBehaviour
{
    public Text score;
    public Player player;
    private void Start()
    {
        score.text = "Score: " + player.score.ToString();
    }

    void Update()
    {
        score.text = "Score: " + player.score.ToString();
    }
}
