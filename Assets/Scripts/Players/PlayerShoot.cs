using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

	public Transform BulletSpawnPosition;
	public GameObject Bullet;
    public PlayerHealth playerHealth;
	public float FireRate = 0.3f;
	private float timeSinceLastFire = Mathf.Infinity;

	public void Shoot()
	{
		if(timeSinceLastFire < FireRate)
		{
			return;
		}
		timeSinceLastFire = 0;

		var bullet = Instantiate(Bullet, BulletSpawnPosition.position, transform.rotation);
		var direction = BulletSpawnPosition.forward;
		direction.y = 0;
		bullet.GetComponent<Bullet>().Setup(direction, playerHealth);
	}

	private void Update()
	{
		timeSinceLastFire += Time.deltaTime;
	}
}
