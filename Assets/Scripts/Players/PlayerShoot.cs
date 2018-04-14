using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

	public Transform BulletSpawnPosition;
	public GameObject Bullet;

	public void Shoot()
	{
		var bullet = Instantiate(Bullet, BulletSpawnPosition.position, transform.rotation);
		var direction = BulletSpawnPosition.forward;
		direction.y = 0;
		bullet.GetComponent<Bullet>().Setup(direction);
	}
}
