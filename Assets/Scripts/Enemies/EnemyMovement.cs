using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public GameObject[] Players;
    public GameObject targetedPlayer;
    public NavMeshAgent navAgent;

	public static bool GameEnded = false;

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

    void Start () {
        if (navAgent == null)
            print("nav agent is not set on enemy");
        targetedPlayer = Players[0];
	}

    private void FixedUpdate()
    {
		if(GameEnded)
		{
			navAgent.isStopped = true;
			return;
		}
        targetedPlayer = SharedMovement.SelectEnemy(gameObject, PlayerManager.Players);
        navAgent.SetDestination(targetedPlayer.transform.position);
    }    
}
