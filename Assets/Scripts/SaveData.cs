using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour {
    [HideInInspector]
    public static SaveData instance;
    [HideInInspector]
    public bool mario_isBig,luigi_isBig, mario_datastored,luigi_datastored,player2spawned;
    [HideInInspector]
    public int mario_life,mario_score,mario_coins, luigi_life, luigi_score,luigi_coins;
    [HideInInspector]
    public int levelreached;

    //non distrugge l'oggetto tra una scena e l'altra
    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
