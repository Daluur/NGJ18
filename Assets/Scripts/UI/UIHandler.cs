using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : Singleton<UIHandler> {

	public SanityBar[] sanityBars;

	public void PlayerWasSpawned(PlayerHealth player, int playerID)
	{
		sanityBars[playerID - 1].AssignPlayer(player);
	}
}
