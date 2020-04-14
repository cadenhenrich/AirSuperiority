using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerController playerController;

    [SerializeField] private float forwardOffset = 50f;
    private Vector2 bounds;

    [SerializeField] private float spawnRate = 10f;
    private float spawnTimer;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject gatlingBullet;
    [SerializeField] private float gatlingOffset;

    [SerializeField] private float enemySpeed;
    [SerializeField] private float enemyFireRate;
    [SerializeField] private float enemyHealth;
    [SerializeField] private float enemyPoints;
    [SerializeField] private float engageDistance;

    private const float verticalOffset = 6.0f;

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
	    Vector3 spawnPosition = new Vector3
	    (
		Random.Range(-bounds.x, bounds.x),
		Random.Range(-bounds.y, bounds.y),
		player.transform.position.z + forwardOffset
	    );

	    GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition + Vector3.up * verticalOffset, Quaternion.identity);
	    Enemy newEnemyController = newEnemy.GetComponent<Enemy>();

	    newEnemyController.SetSpeed(enemySpeed);
	    newEnemyController.SetFireRate(enemyFireRate);
	    newEnemyController.SetHealth(enemyHealth);
	    newEnemyController.SetPoints(enemyPoints);
	    newEnemyController.SetShootingInfo(player, gatlingBullet);
	    newEnemyController.SetEngageDist(engageDistance);

	    spawnTimer = spawnRate;
	}
    }
}
