using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IPlayer {

    public int Sanity = 50;

    public void RewardSanity(int amount)
    {
        Sanity += amount;
    }

    public void TakeDamage(int amount)
	{
        if (Sanity - amount <= 0)
        {
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
}
