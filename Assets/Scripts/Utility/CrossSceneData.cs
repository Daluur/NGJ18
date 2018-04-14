using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossSceneData {

	private static CrossSceneData instance;

	private CrossSceneData() { }

	public static CrossSceneData Instance {
		get {
			if (instance == null)
			{
				instance = new CrossSceneData();
			}
			return instance;
		}
	}

	private List<PlayerControllerData> Players;

	public List<PlayerControllerData> GetPlayers()
	{
		return Players;
	}

	public void UpdatePlayerListOnStartGame(List<PlayerControllerData> players)
	{
		Players = players;
	}
}
