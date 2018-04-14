using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager> {

    public List<GameObject> Players = new List<GameObject>();

	public Sprite[] PlayerSprites;

	public Transform[] PlayerSpawnPositions;
	public GameObject PlayerPrefab;

    public int DepletePerTick = 4;
    public int MaxDepletePerSecond = 8;
    public int DepleteIncreaseInterval = 60;

    public float DepleteInterval = 1;

	private void Awake()
	{
		SpawnPlayers();
	}

	private void SpawnPlayers()
	{
		var players = CrossSceneData.Instance.GetPlayers();
		if (players == null)
		{
			// Game was started from scene.
			// We only add keyboard support.
			var playerObj = Instantiate(PlayerPrefab, PlayerSpawnPositions[0].position, Quaternion.identity);
			playerObj.GetComponent<PlayerController>().Setup(new PlayerControllerData { ControllerID = 0, PlayerID = 1}, PlayerSprites[0]);
			Players.Add(playerObj);
			UIHandler.Instance.PlayerWasSpawned(playerObj.GetComponent<PlayerHealth>(), 1);
		}
		else
		{
			foreach (var player in players)
			{
				// Create each of the players.
				var playerObj = Instantiate(PlayerPrefab, PlayerSpawnPositions[player.PlayerID - 1].position, Quaternion.identity);
				playerObj.GetComponent<PlayerController>().Setup(player, PlayerSprites[player.PlayerID - 1]);
				Players.Add(playerObj);
				UIHandler.Instance.PlayerWasSpawned(playerObj.GetComponent<PlayerHealth>(), player.PlayerID);
			}
		}
	}

	// Use this for initialization
	void Start () {
        StartCoroutine(IncreaseInsanityDepletion());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator IncreaseInsanityDepletion() {
        while (true)
        {
            yield return new WaitForSeconds(DepleteIncreaseInterval);
            DepletePerTick++;
            if (DepletePerTick == MaxDepletePerSecond)
                break;
        }
    }
}
