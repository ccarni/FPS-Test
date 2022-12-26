using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaitingRoom : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private int minimumPlayers = 2;
    [SerializeField] private SpawnPlayers spawnPlayers;
    
    void Awake()
    {
        player = spawnPlayers.Spawn(Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.PlayerList.Length >= minimumPlayers){
            FindObjectOfType<PlayerHealth>().StartGame();
        }
    }

    
}
