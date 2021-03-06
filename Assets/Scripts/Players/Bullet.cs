﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float Speed = 8;
	public int Damage = 10;
	private Vector3 direction;
    private IPlayer player;
	public GameObject WallHitParticles;

	private void Start()
	{
		Destroy(gameObject, 10);
	}

	public void Setup(Vector3 dir, IPlayer player)
	{
        this.player = player;
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
			other.GetComponent<IEnemy>().TakeDamage(Damage, player);
			Destroy(gameObject);
		} else if(other.tag == "Player")
		{
			var otherPlayer = other.GetComponent<IPlayer>();
			if (otherPlayer != player)
			{
				otherPlayer.TakeDamage(Damage, player);
				Destroy(gameObject);
			}
		} else if(other.tag == "Boundary")
		{
			Destroy(gameObject);
		} else if(other.tag == "Wall")
		{
			if (WallHitParticles != null)
			{
				Instantiate(WallHitParticles, transform.position, Quaternion.identity);
			}
			Destroy(gameObject);
		}
	}
}
