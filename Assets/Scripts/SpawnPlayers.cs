using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerOrientation;
    private GameObject playerInstance;
    private GameObject orientation;

    public GameObject Spawn(Vector3 SpawnPoint, Quaternion SpawnRotation)
    {
        playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, SpawnPoint, SpawnRotation);
        playerInstance.GetComponentInChildren<Camera>().enabled =true;
        // orientation = PhotonNetwork.Instantiate(playerOrientation.name, SpawnPoint, SpawnRotation);
        // orientation.transform.SetParent(playerInstance.transform);

        return playerInstance;
    }

    public IEnumerator Respawn(GameObject spectatorCam, float delay, Vector3 RespawnPoint, Quaternion RespawnRotation)
    {
        spectatorCam.SetActive(true);
        PhotonNetwork.Destroy(playerInstance);
        yield return new WaitForSeconds(delay);
        spectatorCam.SetActive(false);
        Spawn(RespawnPoint, RespawnRotation);
    }

}
