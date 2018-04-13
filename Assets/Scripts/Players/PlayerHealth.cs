using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : IPlayer {

	public void TakeDamage(int amount)
	{
		Debug.Log("player recieved " + amount + " 'Damage'");
	}
}
