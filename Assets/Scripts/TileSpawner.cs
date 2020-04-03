using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [Space]

    // References to different types of tiles
    [Header("Tile References")]
    [SerializeField] private GameObject cityTile;
    [SerializeField] private GameObject factoryTile;
    [SerializeField] private GameObject blankTile;
    [SerializeField] private GameObject craterTile;

    [Space]

    // Counters for number of tiles
    [Header("Tile Field Dimensions")]
    [SerializeField] private int rows = 100;
    [SerializeField] private int cols = 4;

    [Space]

    // Determine x and z offset for tiles
    [Header("Offsets")]
    [SerializeField] private float zOffset = 5f;
    [SerializeField] private float xOffset = 4f;

    // Keep track of changing z offset for new tiles
    private float newTileOffset;

    // List to store all spawned tiles in
    private List<GameObject> tiles = new List<GameObject>();

    [Space]

    // Variable to store the tile chances in % out of 100
    [Header("Probabilities")]
    [SerializeField] private float tileChance;
    [SerializeField] private float factoryChance = 10f;

    [Space]

    // Value at which to cull the tiles behind the camera
    [Header("Rendering")]
    [SerializeField] private float cullingValue = 5f;

    // Reference to the camera
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
	// Get camera
	cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

	// Variable to neatify position argument in Instantiate()
	Vector3 position = Vector3.zero;
	bool flipped;

	// Iterate through array and spawn tiles
	for (int row = 0; row < rows; row++)
	{
	    // Set the position to have the proper z-offset
	    position.z = zOffset * row;

	    for (int col = 0; col < cols; col++)
	    {
		// Check if the tile should spawn
		if (Random.value < tileChance / 100f)
		{
		    // Set the position and flipped value for the new tile
		    position.x = (col - (cols / 2)) * xOffset + xOffset;
		    flipped = (Random.value > 0.5f);

		    // Spawn either a factory (never flipped) or a city (sometimes flipped)
		    if (Random.value < factoryChance / 100f)
		        tiles.Add(Instantiate(factoryTile, position, Quaternion.identity));
		    else
			tiles.Add(Instantiate(
			    cityTile, 
			    position, 
			    flipped ? Quaternion.LookRotation(Vector3.back) : Quaternion.identity
			));
		}
	    }
	}

	newTileOffset = zOffset * rows;
    }

    // Update is called once per frame
    void Update()
    {
	// Conditionally cull first row
        for (int col = 0; col < cols; col++)
	{
	    // Check if tile should be culled
	    if (tiles[col].transform.position.z + cullingValue < cam.transform.position.z)
	    {
		Vector3 position = new Vector3(tiles[col].transform.position.x, 0, newTileOffset);
		bool flipped = (Random.value > 0.5f);

		if (Random.value < factoryChance / 100f)
		    tiles.Add(Instantiate(factoryTile, position, Quaternion.identity));
		else
		    tiles.Add(Instantiate(
		        cityTile, 
		        position, 
		        flipped ? Quaternion.LookRotation(Vector3.back) : Quaternion.identity
		    ));

		// Change offset
		newTileOffset += zOffset;

		// Destroy the tile and remove the reference
		Destroy(tiles[col]);
		tiles.RemoveAt(col);
	    }
	}
    }
}
