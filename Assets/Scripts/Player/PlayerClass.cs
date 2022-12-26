using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerClass : MonoBehaviour
{    
    [SerializeField] private float tankWalkSpeed, potatochipWalkSpeed;
    [SerializeField] private float tankSprintSpeed, potatochipSprintSpeed;
    [SerializeField] private float tankHealth, potatochipHealth;
    [SerializeField] private float tankDamage, potatochipDamage;


    [SerializeField] private Canvas chooseClassCanvas;
    [SerializeField] private Gun gun;

    private PlayerCamera playerCamera;
    private Canvas canvas;
    private bool isChoosingClass;

    private void Start()
    {
        playerCamera = FindObjectOfType<PlayerCamera>();
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isChoosingClass){
                Destroy(canvas.gameObject);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                isChoosingClass = false;
                playerCamera.canMoveCamera = true;
            } else {
                canvas = Instantiate(chooseClassCanvas, Vector3.zero, Quaternion.identity);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //basically finds the button and adds Set function to its onClick
                canvas.transform.Find("tank").GetComponent<Button>().onClick.AddListener(SetTank);
                canvas.transform.Find("potatochip").GetComponent<Button>().onClick.AddListener(SetPotatoChip);
                isChoosingClass = true;
                playerCamera.canMoveCamera = false;
            }
        }    
    }

    public void SetTank()
    {
        GetComponent<PlayerMove>().SetSpeeds(tankWalkSpeed, tankSprintSpeed, 60f, 0.4f);
        GetComponent<PlayerHealth>().SetMaxHealth(tankHealth);
        gun.SetGunInfo(tankDamage);
    }

    public void SetPotatoChip()
    {
        GetComponent<PlayerMove>().SetSpeeds(potatochipWalkSpeed, potatochipSprintSpeed, 60f, 0.4f);
        GetComponent<PlayerHealth>().SetMaxHealth(potatochipHealth);
        gun.SetGunInfo(potatochipDamage);
    }

    

}
