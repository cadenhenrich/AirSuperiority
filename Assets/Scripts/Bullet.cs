using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Travel variables
    [Header("Travel")]
    [SerializeField] private float forwardVelocity = 10;
    [SerializeField] private float damage = 1;
    [SerializeField] private float lifetime = 10;

    private PlayerController playerController;

    // Rigidbody reference
    private Rigidbody rb;

    private bool enemyBullet;

    public void SetEnemy(bool enemy)
    {
	enemyBullet = enemy;
    }

    // Called before the first frame
    void Start()
    {
	// Get rb reference
	rb = GetComponent<Rigidbody>();

	rb.isKinematic = true;
	rb.detectCollisions = true;
    }

    // Fixed update is called once every physics cycle (50 times per sec)
    void FixedUpdate()
    { 
	transform.position += transform.forward * forwardVelocity * Time.fixedDeltaTime;
	playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

	lifetime -= Time.fixedDeltaTime;

	if (lifetime <= 0)
	    Destroy(gameObject);
    }

    // Called when the object collides
    void OnTriggerEnter(Collider other)
    {
	if (!enemyBullet)
	{
	    // If the collided object is a tile
	    Tile tile = other.gameObject.GetComponent<Tile>();
	    if (tile != null)
	    {
	        // Damage the tile
	        if (tile.TakeDamage(damage) && !tile.isDestroyed())
		    playerController.AddPoints(tile.getPoints());
		Destroy(gameObject);
	    }
	    else if (other.gameObject.GetComponent<Enemy>())
	    {
	        other.gameObject.GetComponent<Enemy>().takeDamage(damage);
		Destroy(gameObject);
	    }
	}
	else
	{
	    if (other.gameObject.GetComponent<PlayerController>())
	    {
		playerController.takeDamage(damage);
		Destroy(gameObject);
	    }
	}
    }
}
