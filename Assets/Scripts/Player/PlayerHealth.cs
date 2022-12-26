using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float health = 100;

    public int myTeam = 0;
    public Vector3 spawnPoint = Vector3.zero;

    //references
    private GameSetup gs;
    private SpawnPlayers spawnPlayers;
    private PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        gs = FindObjectOfType<GameSetup>();
        spawnPlayers = FindObjectOfType<SpawnPlayers>();
        view = GetComponent<PhotonView>();
        health = maxHealth;
        
        
        if((gs != null) && view.IsMine){
            view.RPC("RPC_GetTeam", RpcTarget.MasterClient);
            Vector3 point;
            
            if(myTeam == 1){
                point = gs.teamOneSpawnPoints[Random.Range(0, gs.teamOneSpawnPoints.Length)].position;
            } else {
                point = gs.teamTwoSpawnPoints[Random.Range(0, gs.teamTwoSpawnPoints.Length)].position;
            }

            GetComponent<Transform>().position = point;
        }
    }

    [PunRPC]
    public void RemoveHealth(float amount)
    {
        health -= amount;
        if(health <= 0) KillPlayer();
    }

    public void StartGame()
    {
        view.RPC("RPC_StartGame", RpcTarget.AllViaServer);
    }

    [PunRPC]
    private void RPC_StartGame()
    {
        PhotonNetwork.LoadLevel("Test");
    }

    public void KillPlayer()
    {
        if(view.IsMine){
            gs.RespawnPlayer(myTeam);
        }

    }

    public void SetHealth(float Health)
    {
        health = Health;
    }

    public void SetMaxHealth(float Health)
    {
        maxHealth = Health;
    }

    [PunRPC]
    void RPC_GetTeam()
    {
        gs = FindObjectOfType<GameSetup>();
        view = GetComponent<PhotonView>();

        myTeam = gs.nextPlayerTeam;
        gs.UpdateTeam();
        view.RPC("RPC_SentTeam", RpcTarget.OthersBuffered, myTeam);
    }

    [PunRPC]
    void RPC_SentTeam(int team)
    {
        myTeam = team;
    }
}
