using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCamera : MonoBehaviour
{
    //public
    public float xSensitivity = 400f, ySensitivity = 400f;
    public bool canMoveCamera = true;

    //other
    private float x, y;
    private float xRot, yRot;

    //references
    public Transform playerOrientation;
    public PhotonView view;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine && canMoveCamera){
            GetInput();
            Rotate();
        }
    }

    void GetInput()
    {
        x = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSensitivity;
        y = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySensitivity;
    }

    void Rotate()
    {
        yRot += x;
        xRot -= y;

        xRot = Mathf.Clamp(xRot, -90f, 90f);

        GetComponent<Transform>().rotation = Quaternion.Euler(xRot, yRot, 0f);
        playerOrientation.rotation = Quaternion.Euler(0f, yRot, 0f);
    }
}
