using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailFollower : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float forwardSpeed = 1;

    // Fixed Update is called every physics iteration (50 times a sec)
    void FixedUpdate()
    {
	transform.position += Vector3.forward * forwardSpeed * Time.fixedDeltaTime;
    }
}
