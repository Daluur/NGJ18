using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    public int Health = 100;
    public GameObject DeathAnim;
    public int Damage = 15;
    public int SanityReward = 1;
	public bool DieOnPlayerHit = true;
	public Animator anim;

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

	private void DoDeath()
    {
        Instantiate(DeathAnim, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<IPlayer>().TakeDamage(Damage);
			if (DieOnPlayerHit)
			{
				DoDeath();
			}
        }
    }
}