using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Possible tile types
    public enum Type
    {
	City,
	Factory,
	Crater
    }

    // Type var to store type of tile
    [Header("Type")]
    [SerializeField] private Type tileType;

    // Model refs
    [Header("Models")]
    [SerializeField] private GameObject aliveModel;
    [SerializeField] private GameObject craterModel;

    // Variable to store health of tile
    [Header("Health")]
    [SerializeField] private float health;

    // Stores the amount of points the player gets
    // upon destroying the tile
    [Header("Points")]
    [SerializeField] private float points;

    // Rigidbody reference
    private Rigidbody rb;

    private AudioSource aS;

    // Called before the first frame
    void Start()
    {
	// Set the proper model to be active
	craterModel.SetActive(false);
	aliveModel.SetActive(true);

	// Get rb reference
	rb = GetComponent<Rigidbody>();

	rb.isKinematic = true;
	rb.detectCollisions = true;

	aS = GetComponent<AudioSource>();
    }

    // Called when the tile should take damage
    public bool TakeDamage(float damage)
    {
	// Damage the tile
	health -= damage;

	// Conditionally destroy the tile
	if (health <= 0 && !isDestroyed())
	{
	    aS.Play();

	    aliveModel.SetActive(false);
	    craterModel.SetActive(true);
	    tileType = Type.Crater;

	    return true;
	}

	return false;
    }

    public float getPoints()
    {
	return points;
    }

    public Type getTileType()
    {
	return tileType;
    }

    public void setTileType(Type newType)
    {
	tileType = newType;
    }

    public bool isDestroyed()
    {
	if (tileType == Type.Crater)
	    return true;

	return false;
    }
}
