﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour {

	private static int MAXNUMBEROFJOYSTICKS = 8;

	public GameObject[] PlayerImages;
	public GameObject PressStartText;

	private List<PlayerControllerData> Players = new List<PlayerControllerData>();

	private void Start()
	{
		Players = CrossSceneData.Instance.GetPlayers();
		if (Players == null)
		{
			Players = new List<PlayerControllerData>();
		}
	}

	// Update is called once per frame
	void Update () {
		for (int i = 1; i <= MAXNUMBEROFJOYSTICKS; i++)
		{
			if (Input.GetButtonDown("Joy" + i + "Shoot") || Input.GetKeyDown(KeyCode.Space)) {
				GotInput(i);
			}
		}
		if (Players.Count > 0)
		{
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("JoyStart"))
			{
				StartGame();
			}
		}
		UpdateUI();
	}

	private void GotInput(int controllerID)
	{
		var existingPlayer = Players.SingleOrDefault(pcd => pcd.ControllerID == controllerID);
		
		if (existingPlayer != null) // Player already added.
		{
			RemoveExistingPlayer(existingPlayer);
		}
		else
		{
			AddNewPlayer(controllerID);
		}
	}

	private void RemoveExistingPlayer(PlayerControllerData pcd)
	{
		Players.Remove(pcd);
	}

	private void AddNewPlayer(int controllerID)
	{
		if (Players.Count == 4)
		{
			return;
		}
		var addPlayerAsID = 0;
		for (int i = 1; i <= 4; i++)
		{
			if (!Players.Any(pcd => pcd.PlayerID == i))
			{
				addPlayerAsID = i;
				break;
			}
		}
		if (addPlayerAsID == 0)
		{
			//There were already 4 players.
			return;
		}
		Players.Add(new PlayerControllerData { ControllerID = controllerID, PlayerID = addPlayerAsID });
	}

	private void UpdateUI()
	{
		var currentlyAssignedPlayers = Players.Select(pcd => pcd.PlayerID);
		for (int i = 0; i < PlayerImages.Length; i++)
		{
			if (currentlyAssignedPlayers.Contains(i + 1))
			{
				PlayerImages[i].SetActive(true);
			}
			else
			{
				PlayerImages[i].SetActive(false);
			}
		}
		if(Players.Count > 0)
		{
			PressStartText.SetActive(true);
		}
		else
		{
			PressStartText.SetActive(false);
		}
	} 

	private void StartGame()
	{
		CrossSceneData.Instance.UpdatePlayerListOnStartGame(Players);
		SceneManager.LoadScene(1);
	}

	public void Quit()
	{
		Application.Quit();
	}
}