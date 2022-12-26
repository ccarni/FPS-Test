using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float health;

    private void Start()
    {
        health = maxHealth;
    }

    public void RemoveHealth(float amount)
    {
        health -= amount;
        if(health <= 0){
            Destroy(gameObject);
        }
    }
}
