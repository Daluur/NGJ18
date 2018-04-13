using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float Speed = 8;
	public int Damage = 10;
	private Vector3 direction;

	public void Setup(Vector3 dir)
	{
		direction = dir * Speed;
	}

	private void Update()
	{
		transform.Translate(direction * Time.deltaTime);
	}

	public void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Enemy")
		{
			other.GetComponent<IEnemy>().TakeDamage(Damage);
			Destroy(gameObject);
		}
	}
}
