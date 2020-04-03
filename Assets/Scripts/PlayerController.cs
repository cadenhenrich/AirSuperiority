using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player movement variables
    private Vector3 centerPosition;
    private Vector3 displacement;
    private Vector3 velocity;

    [Header("Movement")]
    [SerializeField] private float maxVelocity = 2.0f;
    [SerializeField] private float acceleration = 2.0f;

    [Space]

    [Header("Restraints")]
    [SerializeField] private Vector2 playerBounds;
    private Vector3 relativeMousePosition;

    [Space]

    // Player rotation variables
    private Vector3 lookPosition;
    private int layerMask = 1 << 8;

    [Header("Rotation")]
    [SerializeField] private float maxLookRange;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float maxRoll = 15f;

    // Reference to the tracking objects for the player
    private GameObject railFollower;
    private GameObject playerModel;

    // Camera vars
    private Camera cam;

    private void MovePlayer()
    {
	// Get the mouse position relative to the center of the screen
	relativeMousePosition.x = -1.0f + Input.mousePosition.x * 2.0f / Screen.width;
	relativeMousePosition.y = -1.0f + Input.mousePosition.y * 2.0f / Screen.height;

	// Get the displacement from the player's position it needs to move
	displacement = new Vector3
	(
	    relativeMousePosition.x * playerBounds.x,
	    relativeMousePosition.y * playerBounds.y,
	    0
	);

	// Set the player's velocity to a fraction of the displacement
	velocity = displacement * acceleration;

	// Clamp the velocity to be under maxVelocity
	velocity = Vector3.ClampMagnitude(velocity, maxVelocity);

	// Change the player's position based on the velocity
	transform.position += velocity * Time.fixedDeltaTime;

	// Clamp the position to be inside the playerBounds
	if (transform.position.x > centerPosition.x + playerBounds.x)
	    transform.position = new Vector3(centerPosition.x + playerBounds.x, transform.position.y, transform.position.z);
	else if (transform.position.x < centerPosition.x - playerBounds.x)
	    transform.position = new Vector3(centerPosition.x - playerBounds.x, transform.position.y, transform.position.z);

	if (transform.position.y > centerPosition.y + playerBounds.y)
	    transform.position = new Vector3(transform.position.x, centerPosition.y + playerBounds.y, transform.position.z);
	else if (transform.position.y < centerPosition.y - playerBounds.y)
	    transform.position = new Vector3(transform.position.x, centerPosition.y - playerBounds.y, transform.position.z);
    }

    private void RotatePlayer()
    {
	// Make a ray from the camera to a point some distance away
	RaycastHit hit;
	Ray ray = cam.ScreenPointToRay(Input.mousePosition);

	// Raycast along that ray for maxLookRange, ignoring the player
	if (Physics.Raycast(ray, out hit, maxLookRange, layerMask))
	{
	    // Set the rotation to face that ray
	    playerModel.transform.rotation = Quaternion.RotateTowards(
	        playerModel.transform.rotation, 
	        Quaternion.LookRotation((hit.point - playerModel.transform.position).normalized), 
	        Mathf.Deg2Rad * rotationSpeed * (1.0f / Time.fixedDeltaTime)
	    );
	}
	else
	{
	    // Set the rotation back to normal
	    playerModel.transform.rotation = Quaternion.RotateTowards(
	        playerModel.transform.rotation, 
	        Quaternion.LookRotation(Vector3.forward), 
	        Mathf.Deg2Rad * rotationSpeed * (1.0f / Time.fixedDeltaTime)
	    );
	}

	// Roll the player based on their current velocity along the x-axis
	Vector3 currentRotation = playerModel.transform.localEulerAngles;
	currentRotation.z = (-velocity.x / maxVelocity) * maxRoll;
	playerModel.transform.localEulerAngles = currentRotation;
    }

    // Start is called before the first frame update
    void Start()
    {
	// Get the objects by tag
	railFollower = GameObject.FindGameObjectWithTag("RailFollower");

	// Get the camera
	cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	
	// Get the player model object
	playerModel = GameObject.FindGameObjectWithTag("Model");

	// Get the player's initial position and screen center
	centerPosition = new Vector3(transform.position.x, transform.position.y, 0);

	// Set the layer mask
	layerMask = ~layerMask;
    }


    // FixedUpdate is called every physics cycle (50 times per sec)
    void FixedUpdate()
    {
	MovePlayer();
	RotatePlayer();
    }

    void OnCollisionEnter(Collision collision)
    {
	Debug.Log("OUCH OOF OWIE THAT HURT");
    }
}
