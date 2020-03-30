using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player movement variables
    [SerializeField] private float movementLerpFactor = 1.0f;

    private Vector3 displacement;
    private Vector3 velocity;

    [SerializeField] private float maxVelocity = 2.0f;
    [SerializeField] private float acceleration = 2.0f;
    [SerializeField] private float edgeDistanceCutoff = 1.0f;
    [SerializeField] private float rotationSpeed = 1f;

    // Stores the last good player mouse pos
    private Vector3 lastHitPosition;

    // Reference to the tracking objects for the player
    private GameObject railFollower;
    private GameObject targetPlane;
    private GameObject playerModel;

    // Camera vars
    private Camera cam;
    [SerializeField] private Vector2 cameraBounds;

    private void MovePlayer()
    {
	// Raycast variables from camera to point in world space
	RaycastHit hit;
	Ray ray = cam.ScreenPointToRay(Input.mousePosition);

	// Check the hit object == the target plane
	if (Physics.Raycast(ray, out hit) && hit.collider.tag == "TargetPlane")
	{
	    // Store the hit position
	    lastHitPosition = hit.point;

	    // Calculate the displacement 
	    Vector3 planarPosition = new Vector3(hit.point.x, hit.point.y, transform.position.z);
	    displacement = Vector3.Lerp(transform.position, planarPosition, movementLerpFactor * Time.fixedDeltaTime);

	    // Accelerate the body
	    velocity += (displacement - transform.position) * acceleration;

	    // Clamp the velocity
	    if (velocity.magnitude > maxVelocity)
	    {
		velocity = velocity.normalized * maxVelocity;
	    }
	}
	else
	{
	    // Do the same as above but with the last hit position instead
	    Vector3 planarPosition = new Vector3(lastHitPosition.x, lastHitPosition.y, transform.position.z);
	    displacement = Vector3.Lerp(transform.position, planarPosition, movementLerpFactor);

	    // Check to see if we should stop using acceleration and just manually set the velocity
	    if ((planarPosition - transform.position).magnitude > edgeDistanceCutoff)
	    {
		velocity += displacement - transform.position;
	    	if (velocity.magnitude > maxVelocity)
	    	{
	    	    velocity = velocity.normalized * maxVelocity;
	    	}
	    }
	    else
	    {
		velocity = displacement - transform.position;
	    	if (velocity.magnitude > maxVelocity)
	    	{
	    	    velocity = velocity.normalized * maxVelocity;
	    	}
	    }
	}

	// Add the velocity to the position to move the player
	transform.position += velocity * Time.fixedDeltaTime;

	// Rotate the player to face the point it's traveling towards
	// playerModel.transform.rotation = Quaternion.RotateTowards(playerModel.transform.rotation, Quaternion.LookRotation(hit.point), Mathf.Deg2Rad * rotationSpeed * Time.fixedDeltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
	// Get the objects by tag
	railFollower = GameObject.FindGameObjectWithTag("RailFollower");
	targetPlane = GameObject.FindGameObjectWithTag("TargetPlane");

	// Get the camera
	cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	
	// Get the player model object
	playerModel = GameObject.FindGameObjectWithTag("Model");
    }


    // FixedUpdate is called every physics cycle (50 times per sec)
    void FixedUpdate()
    {
	MovePlayer();
    }

    void OnCollisionEnter(Collision collision)
    {
	Debug.Log("OUCH OOF OWIE THAT HURT");
    }
}
