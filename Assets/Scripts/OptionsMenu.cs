using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
    private CameraController cam;
    [Header("Add Player 2 settings")]
    public Player player2;
    private Player player1;
    public Vector3 player2SpawnOffset;
    private Transform player2transform;
    public Button add, disable;

    [Header("Altro")]
    public int scr;

    public void Awake()
    {
        if (SaveData.instance.player2spawned)
        {
            add.gameObject.SetActive(false);
            disable.gameObject.SetActive(true);
        }
        else
        {
            add.gameObject.SetActive(true);
            disable.gameObject.SetActive(false);
        }
        player1 = GameObject.Find("Mario").GetComponent<Player>();
        cam = FindObjectOfType<Camera>().GetComponent<CameraController>();
    }

    //aggiunge il player 2 al gioco
    public void AddPlayer()
    {
        if (!SaveData.instance.player2spawned)
        {
            Vector3 spawnPosition = player1.transform.position + player2SpawnOffset;
            Quaternion rotation = player1.transform.rotation;
            player2transform=Instantiate(player2, spawnPosition, rotation).GetComponent<Transform>();
            cam.targets.Add(player2transform);
            add.gameObject.SetActive(false);
            disable.gameObject.SetActive(true);
            SaveData.instance.player2spawned = true;
        }
    }

    //disabilita il player 2 dal gioco
    public void DisablePlayer()
    {
        try
        {
            DestroyImmediate(player2transform.gameObject);
            cam.targets.Remove(player2transform);
            disable.gameObject.SetActive(false);
            add.gameObject.SetActive(true);
            SaveData.instance.player2spawned = false;
        }
        catch (Exception e)
        {
            DestroyImmediate(GameObject.Find("Luigi"));
            cam.targets.Remove(player2transform);
            disable.gameObject.SetActive(false);
            add.gameObject.SetActive(true);
            SaveData.instance.player2spawned = false;
        }
    }
}
