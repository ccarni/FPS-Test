using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private RaycastHit hit;

    [SerializeField] private Camera playerCam;

    [SerializeField] private float range;
    [SerializeField] private LayerMask whatIsInteractable;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range, whatIsInteractable) && Input.GetKey(interactKey)){
            hit.collider.GetComponent<Interactable>().Interact(gameObject);
        }
    }
}
