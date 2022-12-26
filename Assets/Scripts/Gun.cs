using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gun : MonoBehaviour
{
    //references
    [SerializeField] private Transform camController;
    [SerializeField] private ParticleSystem muzzleFlash;

    //data
    [SerializeField] private  float range;
    [SerializeField] private float damagePerHit;
    [SerializeField] private LayerMask whatCanShoot;

    private RaycastHit shootHitInfo;
    
    
    public void Shoot()
    {
        bool hit = Physics.Raycast(camController.position, camController.forward, out shootHitInfo, range, whatCanShoot);
        muzzleFlash.Play();
        
        if(hit){
            if(shootHitInfo.transform.GetComponent<Target>() != null)
                shootHitInfo.transform.GetComponent<Target>().RemoveHealth(damagePerHit);

            if(shootHitInfo.transform.GetComponent<PlayerHealth>() != null)
                shootHitInfo.transform.GetComponent<PhotonView>().RPC("RemoveHealth", RpcTarget.AllBuffered, damagePerHit);
        }
    }

    private void Update() 
    {
        if(Input.GetMouseButtonDown(0)){
            Shoot();
        }
    }

    public void SetGunInfo(float Damage)
    {
        damagePerHit = Damage;
    }

}
