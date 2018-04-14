using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerManager : MonoBehaviour {

    public List<GeneralPlayer> Players = new List<GeneralPlayer>();

    public const string scoreString = "";
    public GameObject loseScreen;
    public Text ScoreText, FinalScoreText;
    [HideInInspector]
    public int Score;

	public Sprite[] PlayerSprites;

	public Transform[] PlayerSpawnPositions;
	public GameObject PlayerPrefab;
	public GameObject PlayerMoveParticles;

    public int DepletePerTick = 4;
    public int MaxDepletePerSecond = 8;
    public int DepleteIncreaseInterval = 60;

    public float DepleteInterval = 1;

    private UIHandler _UIHandler;
    public UIHandler UIHandler {
    get {
            if(_UIHandler == null)
            {
                _UIHandler = GameObject.FindGameObjectWithTag("UIHandler").GetComponent<UIHandler>();
            }
            return _UIHandler;
        }
    }

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
			ActualSpawn(new PlayerControllerData { ControllerID = 0, PlayerID = 1 });
		}
		else
		{
			foreach (var player in players)
			{
				ActualSpawn(player);
			}
		}
	}

	private void ActualSpawn(PlayerControllerData player) 
	{
		// Create each of the players.
		var playerObj = Instantiate(PlayerPrefab, PlayerSpawnPositions[player.PlayerID - 1].position, Quaternion.identity);
		playerObj.GetComponent<PlayerController>().Setup(player, PlayerSprites[player.PlayerID - 1]);

		var generalPlayer = playerObj.GetComponent<GeneralPlayer>();
		Players.Add(generalPlayer);

		var particles = Instantiate(PlayerMoveParticles, PlayerSpawnPositions[player.PlayerID - 1].position, Quaternion.identity);
		particles.GetComponent<FollowPlayer>().player = generalPlayer;

        UIHandler.PlayerWasSpawned(playerObj.GetComponent<PlayerHealth>(), player.PlayerID);
	}

	// Use this for initialization
	void Start () {
        StartCoroutine(IncreaseInsanityDepletion());
        gameStartTime = Time.timeSinceLevelLoad;
    }

    private float gameStartTime;
	// Update is called once per frame
	void Update () {
        if (Players.Count == 0 || Players.Any(p => !p.playerHealth.GetIsInsane()))
        {
            Score += (int)(Time.timeSinceLevelLoad - gameStartTime) * 10;
            ScoreText.text = scoreString + Score;
            return;
        }
        else {
            ScoreText.gameObject.SetActive(false);
            FinalScoreText.text = scoreString + Score;
            loseScreen.SetActive(true);
            Time.timeScale = 0.0f;
        }
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

    public void OnDestroy()
    {
        StopAllCoroutines();
    }
}
