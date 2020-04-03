using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // reference to player
    private GameObject player; 

    // determine bounds and offset for camera
    [SerializeField] private Vector2 cameraBounds; 
    [SerializeField] private float cameraFollowSpeed;
    private Vector3 cameraOffset;
    private Vector3 centerPosition;

    // Start is called before the first frame update
    void Start()
    {
	// Reference to the player object
	player = GameObject.FindGameObjectWithTag("Player");

	// Offset relative to player
	cameraOffset = transform.position - player.transform.position;

	// Center position
	centerPosition = new Vector3(transform.position.x, transform.position.y, 0);
    }

    // Fixed update is called every physics iteration (50 times per sec)
    void FixedUpdate()
    {
	// Get the desired position
	Vector3 desiredPosition = player.transform.position + cameraOffset;

	// Lerp to the desired position
	transform.position = Vector3.Lerp(transform.position, desiredPosition, cameraFollowSpeed * Time.fixedDeltaTime);

	// Clamp the camera to the cameraBounds
	if (transform.position.x > centerPosition.x + cameraBounds.x)
	    transform.position = new Vector3(centerPosition.x + cameraBounds.x, transform.position.y, transform.position.z);
	else if (transform.position.x < centerPosition.x - cameraBounds.x)
	    transform.position = new Vector3(centerPosition.x - cameraBounds.x, transform.position.y, transform.position.z);

	if (transform.position.y > centerPosition.y + cameraBounds.y)
	    transform.position = new Vector3(transform.position.x, centerPosition.y + cameraBounds.y, transform.position.z);
	else if (transform.position.y < centerPosition.y - cameraBounds.y)
	    transform.position = new Vector3(transform.position.x, centerPosition.y - cameraBounds.y, transform.position.z);
    }
}
