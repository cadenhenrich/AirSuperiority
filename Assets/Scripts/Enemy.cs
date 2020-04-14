using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed;
    private float fireRate;
    private float health;
    private float points;
    private float engageDist;

    private float shootingTimer;

    private GameObject player;
    private GameObject gatlingBullet;

    private GameObject gatlingGun;

    private RailFollower rail;

    public void SetSpeed(float newSpeed)
    {
	speed = newSpeed;
    }

    public void SetFireRate(float newFireRate)
    {
	fireRate = newFireRate;
    }

    public void SetEngageDist(float newEngage)
    {
	engageDist = newEngage;
    }

    public void SetHealth(float newHealth)
    {
	health = newHealth;
    }

    public void SetPoints(float newPoints)
    {
	points = newPoints;
    }

    public void SetShootingInfo(GameObject newPlayer, GameObject newGatlingBullet)
    {
	player = newPlayer;
	gatlingBullet = newGatlingBullet;
    }

    public void takeDamage(float damage)
    {
	health -= damage;

	if (isDead())
	{
	    player.GetComponent<PlayerController>().AddPoints(points);
	    Destroy(gameObject);
	}
    }

    public bool isDead()
    {
	if (health <=0)
	    return true;

	return false;
    }

    void Move()
    {
	transform.position += Vector3.back * speed * Time.fixedDeltaTime;
    }

    void Shoot()
    {
	Vector3 target = player.transform.position + Vector3.forward * rail.GetForwardSpeed() - transform.position;
	if (target.magnitude < engageDist)
	{
	    GameObject bullet = Instantiate(gatlingBullet, gatlingGun.transform.position, Quaternion.LookRotation(target, Vector3.forward));
	    Bullet controller = bullet.GetComponent<Bullet>();
	    controller.SetEnemy(true);
	}
    }

    void Start()
    {
	gatlingGun = transform.Find("EnemyGatling").gameObject;
	rail = GameObject.FindGameObjectWithTag("RailFollower").GetComponent<RailFollower>();
    }

    void FixedUpdate()
    {
	Move();

	shootingTimer -= Time.fixedDeltaTime;

	if (shootingTimer <= 0 && transform.position.z > player.transform.position.z)
	{
	    Shoot();

	    shootingTimer = 1.0f / fireRate;
	}

	if (transform.position.z + 10.0f < player.transform.position.z)
	{
	    Destroy(gameObject);
	}
    }
}
