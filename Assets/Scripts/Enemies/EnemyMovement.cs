using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public GameObject[] Players;
    public GameObject targetedPlayer;
    public NavMeshAgent navAgent;

    void Start () {
        if (navAgent == null)
            print("nav agent is not set on enemy");
        targetedPlayer = Players[0];
	}

    private void FixedUpdate()
    {
        targetedPlayer = SharedMovement.SelectEnemy(gameObject);
        navAgent.SetDestination(targetedPlayer.transform.position);
    }    
}
