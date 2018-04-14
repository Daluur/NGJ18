using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : GeneralPlayer, IPlayer {

    public int Sanity = 50;
    [HideInInspector]
    public bool IsInsane = false;
    public float BreakingAnimTimeStart = 1f, BreakingAnimTimeEnd = 1f;


    public void RewardSanity(int amount)
    {
        Sanity += amount;
    }

    public void TakeDamage(int amount)
	{
        if (Sanity - amount <= 0)
        {
            IsInsane = true;
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
            if(Sanity - PlayerManager.Instance.DepletePerTick <= 0)
            {
                //TODO: Trigger the breaking
                Sanity = 0;
            }
            else { 
                Sanity -= PlayerManager.Instance.DepletePerTick;
            }
        }
    }

	public void TakeDamage(int amount, IPlayer player)
	{
		TakeDamage(amount);
		player.TakeDamage(amount);
	}

    private void StartBreakingSequence() {
        IsInsane = true;
    }

    private IEnumerator BreakingSequence() {
        yield return new WaitForSeconds(BreakingAnimTimeStart);
        //TODO: Do insanity things
        yield return new WaitForSeconds(BreakingAnimTimeEnd);
        IsInsane = false;
    }

    private void FixedUpdate()
    {
        if (IsInsane)
        {

        }
    }

    public bool GetIsInsane()
    {
        return IsInsane;
    }
}
