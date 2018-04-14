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
        SelecteEnemy();
        navAgent.SetDestination(targetedPlayer.transform.position);
    }

    private void SelecteEnemy() {
        var enemyPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
        float closesDist = float.MaxValue, dist;
        GameObject closestPlayer = Players[0];
        foreach (var player in Players)
        {
            dist = Vector2.Distance(enemyPos, new Vector2(player.transform.position.x, player.transform.position.z));
            if (Mathf.Abs(dist) < closesDist)
            {
                closestPlayer = player;
                closesDist = dist;
            }
        }
        targetedPlayer = closestPlayer;
    }
    
}
