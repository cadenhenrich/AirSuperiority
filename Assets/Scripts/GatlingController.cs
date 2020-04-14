using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
	transform.RotateAround(transform.position, transform.up, rotationSpeed);
    }
}
