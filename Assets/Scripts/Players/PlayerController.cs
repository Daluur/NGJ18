using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : GeneralPlayer {

	public float MoveSpeed = 3;
	public Transform GunModel;
	public PlayerShoot Gun;

	public Rigidbody Body;
	public NavMeshAgent Agent;
	public SpriteRenderer Renderer;

    public IPlayer player;
    public float InsaneMovementInfluenceFactor = 0.3f;

	public int controllerID = 0;
	public int playerID = 0;

    [HideInInspector]
	public bool isShooting;
	public GameObject shootParticles;

	private string moveXString;
	private string moveYString;
	private string torsoXString;
	private string torsoYString;
	private string shootString;

    private PlayerManager _playerManager;
    public PlayerManager PlayerManager
    {
        get
        {
            if (_playerManager == null)
            {
                _playerManager = GameObject.FindGameObjectWithTag("Playermanager").GetComponent<PlayerManager>();
            }
            return _playerManager;
        }
    }

    public void Setup(PlayerControllerData data, Sprite sprite)
	{
		controllerID = data.ControllerID;
		playerID = data.PlayerID;
		Renderer.sprite = sprite;
		SetupInputVariables();
        player = gameObject.GetComponent<IPlayer>();
	}

	private void SetupInputVariables()
	{
		if (controllerID == 0)
		{
			moveXString = "KeyboardMoveX";
			moveYString = "KeyboardMoveY";
			torsoXString = "KeyboardTorsoX";
			torsoYString = "KeyboardTorsoY";
			shootString = "KeyboardShoot";
		}
		else
		{
			moveXString = "Joy" + controllerID + "MoveX";
			moveYString = "Joy" + controllerID + "MoveY";
			torsoXString = "Joy" + controllerID + "TorsoX";
			torsoYString = "Joy" + controllerID + "TorsoY";
			shootString = "Joy" + controllerID + "Shoot";
		}
	}

	// Update is called once per frame
	void Update () {
        if (playerHealth.InsaneConversionAnimPlaying)
        {
            return;
        }

        var xAxis = Input.GetAxis(moveXString) * MoveSpeed * Time.deltaTime;
        var yAxis = Input.GetAxis(moveYString) * MoveSpeed * Time.deltaTime;

        if (player.GetIsInsane())
        {
            gameObject.transform.position += new Vector3(xAxis, 0, yAxis) * InsaneMovementInfluenceFactor;
            Agent.enabled = true;
            var targetedPlayer = SharedMovement.SelectEnemy(gameObject, PlayerManager.Players);
            Agent.SetDestination(targetedPlayer.gameObject.transform.position);
            return;
        }
        else
        {
			transform.rotation = Quaternion.identity;
            Agent.enabled = false;
        }

		Body.MovePosition(transform.position + new Vector3(xAxis, 0, yAxis));

		var torsoX = Input.GetAxis(torsoXString);
		var torsoY = Input.GetAxis(torsoYString);

		if (torsoX + torsoY != 0)
		{
			GunModel.eulerAngles = new Vector3(GunModel.eulerAngles.x, Mathf.Atan2(torsoX, torsoY) * Mathf.Rad2Deg, GunModel.eulerAngles.z);
		}

		if (Input.GetButton(shootString))
		{
			if(!isShooting)
			{
				isShooting = true;
				shootParticles.SetActive(true);
			}
			Gun.Shoot();
		}
		else if(isShooting)
		{
			isShooting = false;
			shootParticles.SetActive(false);
		}
	}
}
