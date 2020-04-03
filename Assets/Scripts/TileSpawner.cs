using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    // References to different types of tiles
    [SerializeField] private GameObject cityTile;
    [SerializeField] private GameObject factoryTile;
    [SerializeField] private GameObject blankTile;
    [SerializeField] private GameObject craterTile;

    // Counters for number of tiles
    private const int rows = 40;
    private const int cols = 4;

    // Determine x and z offset for tiles
    [SerializeField] private float zOffset = 5f;
    [SerializeField] private float xOffset = 4f;

    // List to store all spawned tiles in
    GameObject[,] tiles = new GameObject[rows,cols];

    // Variable to store the tile chances in % out of 100
    [SerializeField] private float tileChance;
    [SerializeField] private float factoryChance = 10f;

    // Start is called before the first frame update
    void Start()
    {
	// Variable to neatify position argument in Instantiate()
	Vector3 position = Vector3.zero;
	bool flipped;

	// Iterate through array and spawn tiles, mate
	for (int row = 0; row < rows; row++)
	{
	    position.z = zOffset * row;

	    for (int col = 0; col < cols; col++)
	    {
		if (Random.value < tileChance / 100f)
		{
		    position.x = (col - (cols / 2)) * xOffset + xOffset;
		    flipped = (Random.value > 0.5f);

		    if (Random.value < factoryChance / 100f)
		        tiles[row,col] = Instantiate(factoryTile, position, Quaternion.identity);
		    else
		        tiles[row,col] = Instantiate
		        (
		    	cityTile, 
		    	position, 
		    	flipped ? Quaternion.LookRotation(Vector3.back) : Quaternion.identity
		        );
		}
	    }
	}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
