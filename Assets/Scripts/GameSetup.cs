using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameSetup : MonoBehaviour
{
    //public
    public int nextPlayerTeam;
    public Transform[] teamOneSpawnPoints, teamTwoSpawnPoints;

    //references
    private PhotonView view;
    [SerializeField] SpawnPlayers spawnPlayers;
    [SerializeField] private GameObject respawnCamera;
    
    // Start is called before the first frame update

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        spawnPlayers.Spawn(Vector3.zero, Quaternion.identity);
    }

    public void UpdateTeam()
    {
        if(nextPlayerTeam == 1){
            nextPlayerTeam = 2;
        } else {
            nextPlayerTeam = 1;
        }
    }

    public void RespawnPlayer(int team)
    {
        Vector3 point;
        if(team == 1){
            point = teamOneSpawnPoints[Random.Range(0, teamOneSpawnPoints.Length)].position;
        } else {
            point = teamTwoSpawnPoints[Random.Range(0, teamTwoSpawnPoints.Length)].position;
        }
        FindObjectOfType<SpawnPlayers>().StartCoroutine(FindObjectOfType<SpawnPlayers>().Respawn(respawnCamera, 5f, point, Quaternion.identity));
    }

    public void TeamWon(int team)
    {
        view.RPC("RPC_TeamWon", RpcTarget.AllBufferedViaServer, team);
    }

    [PunRPC]
    private void RPC_TeamWon(int team)
    {
        Debug.Log("Team " + team + " Wins!");
        PhotonNetwork.LoadLevel("Menu");
    }

    private void OnPlayerConnected(Photon.Realtime.Player newPlayer) 
    {
    
    }
}
