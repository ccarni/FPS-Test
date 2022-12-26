using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform objectToFollow;

    void Update()
    {
        GetComponent<Transform>().position = objectToFollow.position;
    }
}
