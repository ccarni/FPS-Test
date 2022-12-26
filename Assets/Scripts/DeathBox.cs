using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DeathBox : MonoBehaviour
{
    [SerializeField] private Vector3 respawnPoint;

    private void OnCollisionEnter(Collision other) 
    {
        other.collider.gameObject.GetComponent<Transform>().position = respawnPoint;
    }
}
