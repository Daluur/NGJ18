using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    public int Health = 100;
    public GameObject DeathAnim;
    public int Damage = 15;
    public int SanityReward = 1;
	public bool DieOnPlayerHit = true;
	public Animator anim;

	public AudioClip audioClip;

	[HideInInspector]
	public AudioManager audioManager;
	private AudioSource audioScource;

	public GameObject[] Drops;
	public float[] DropsProbability;

	void Awake()
	{
		audioScource = gameObject.GetComponent<AudioSource> ();
		audioManager = GameObject.Find ("AudioManager").GetComponent<AudioManager> ();
	} 

    public void TakeDamage(int amount, IPlayer player)
    {
        if (Health - amount <= 0)
        {
            player.RewardSanity(SanityReward);
            DoDeath();
            return;
        }
        Health = Health - amount;
		anim.SetTrigger("TookDamage");
    }

	public bool IsBoss()
	{
		return !DieOnPlayerHit;
	}

	private void DoDeath()
    {
		DoDrops();
        Instantiate(DeathAnim, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

	private void DoDrops()
	{
		if(Drops.Length == 0)
		{
			return;
		}
		var roll = Random.Range(0f, 1f);
		var combinedChance = 0f;
		for (int i = 0; i < Drops.Length; i++)
		{
			combinedChance += DropsProbability[i];
			if (combinedChance > roll)
			{
				DropThing(Drops[i]);
				return;
			}
		}
	}

	private void DropThing(GameObject go)
	{
		if (go.tag == "Potion")
		{
			Instantiate(go, transform.position, Quaternion.identity);
		}
		if (go.tag == "Enemy")
		{
			Spawner.ActualSpawnEnemy(this.transform.position, this.GetComponent<EnemyMovement>().Players.Select(p => p.GetComponent<GeneralPlayer>()).ToList(), go.GetComponent<Enemy>(), 1);
		}
	}

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
			audioManager.PlaySound (audioScource, audioClip, 1f, true);
            collision.gameObject.GetComponent<IPlayer>().TakeDamage(Damage);
			if (DieOnPlayerHit)
			{
				DoDeath();
			}
        }
    }
}