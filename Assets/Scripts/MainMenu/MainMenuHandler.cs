using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour {

	private static int MAXNUMBEROFJOYSTICKS = 8;

	public GameObject[] PlayerImages;
	public GameObject[] JoinText;
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

        if (Input.GetKeyDown(KeyCode.C))
        {
            Spawner.FullRandomSpawn = !Spawner.FullRandomSpawn;
            Debug.Log("Fullrandom mode is: " + Spawner.FullRandomSpawn);
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
		var playerPanelsNOTActive = new List<int>();
		for (int i = 0; i < PlayerImages.Length; i++)
		{
			if (currentlyAssignedPlayers.Contains(i + 1))
			{
				PlayerImages[i].SetActive(true);
			}
			else
			{
				PlayerImages[i].SetActive(false);
				playerPanelsNOTActive.Add(i);
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
		for (int i = 0; i < JoinText.Length; i++)
		{
			JoinText[i].SetActive(false);
		}
		if (playerPanelsNOTActive.Count > 0)
		{
			var textToShow = playerPanelsNOTActive.OrderBy(i => i).First();
			JoinText[textToShow].SetActive(true);
		}
	} 

	private void StartGame()
	{
		CrossSceneData.Instance.UpdatePlayerListOnStartGame(Players);
		SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 2);
	}

	public void PlayAloneWithKeyboard()
	{
		Players = new List<PlayerControllerData>();
		Players.Add(new PlayerControllerData { ControllerID = 0, PlayerID = 0 });
		CrossSceneData.Instance.UpdatePlayerListOnStartGame(Players);
		SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 2);
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void Credits()
	{
		SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
	}
}