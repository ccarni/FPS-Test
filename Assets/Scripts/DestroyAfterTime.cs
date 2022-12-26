using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    
    public float time;
    
    void Start()
    {
        Invoke("Destroy", time);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

}
