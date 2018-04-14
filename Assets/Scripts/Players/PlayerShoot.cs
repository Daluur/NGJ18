using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

	public Transform BulletSpawnPosition;
	public GameObject[] Bullets;
    public PlayerHealth playerHealth;
	public float FireRate = 0.3f;
	private float timeSinceLastFire = Mathf.Infinity;
	private int lastBulletTypeFired = 0;
	private int amountOfBullets;

	private void Start()
	{
		amountOfBullets = Bullets.Length - 1;
	}

	public void Shoot()
	{
		if(timeSinceLastFire < FireRate)
		{
			return;
		}
		timeSinceLastFire = 0;

		var bullet = Instantiate(Bullets[lastBulletTypeFired++], BulletSpawnPosition.position, transform.rotation);
		var direction = BulletSpawnPosition.forward;
		direction.y = 0;
		bullet.GetComponent<Bullet>().Setup(direction, playerHealth);
		if(lastBulletTypeFired > amountOfBullets)
		{
			lastBulletTypeFired = 0;
		}
	}

	private void Update()
	{
		timeSinceLastFire += Time.deltaTime;
	}
}
