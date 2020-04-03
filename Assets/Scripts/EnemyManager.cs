using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerController playerController;

    [Space]

    [Header("Spawn Variables")]
    [SerializeField] private float forwardOffset = 50f;
    private Vector2 bounds;

    [SerializeField] private float spawnRate = 10f;
    private float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
	bounds = playerController.GetBounds();

	spawnTimer = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;

	if (spawnTimer <= 0)
	{
	}
    }
}
