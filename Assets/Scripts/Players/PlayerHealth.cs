using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : GeneralPlayer, IPlayer {

    public int MaxSanity = 100;
    public int Sanity = 50;
    [HideInInspector]
    public bool IsInsane = false;
    [HideInInspector]
    public bool InsaneConversionAnimPlaying;
    public float BreakingAnimTimeStart = 1f, BreakingAnimTimeEnd = 1f;
    public float BreakDuration = 10f;
    public int InsanityDamage = 120;

    public float InsanityStartTime;


    public void RewardSanity(int amount)
    {
        Sanity += amount;
    }

    public void TakeDamage(int amount)
	{
        if (Sanity - amount <= 0)
        {
            StartBreakingSequence();
            Sanity = 0;
        }
        else
            Sanity -= amount;
    }

    void Start() {
        StartCoroutine(DepleteSanity());
    }

    private IEnumerator DepleteSanity() {
        while (true) {
            yield return new WaitForSeconds(PlayerManager.Instance.DepleteInterval);
            TakeDamage(PlayerManager.Instance.DepletePerTick);
        }
    }

	public void TakeDamage(int amount, IPlayer player)
	{
		TakeDamage(amount);
		player.TakeDamage(amount);
	}

    private void StartBreakingSequence() {
        if (IsInsane)
            return;
        IsInsane = true;
        Debug.Log("IsInsane");
        StartCoroutine(BreakingSequence());
    }

    private IEnumerator BreakingSequence() {
        InsaneConversionAnimPlaying = true;
        yield return new WaitForSeconds(BreakingAnimTimeStart);
        InsaneConversionAnimPlaying = false;
        InsanityStartTime = Time.timeSinceLevelLoad;
        yield return new WaitForSeconds(BreakDuration);
        InsaneConversionAnimPlaying = true;
        yield return new WaitForSeconds(BreakingAnimTimeEnd);
        InsaneConversionAnimPlaying = false;
        IsInsane = false;
        Sanity = (int)(MaxSanity * 0.2f);
    }

    public bool GetIsInsane()
    {
        return IsInsane;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            var otherPlayer = collision.gameObject.GetComponent<IPlayer>();
            if (otherPlayer.GetIsInsane()) {
                this.TakeDamage(InsanityDamage);
            }
        }
    }
}
