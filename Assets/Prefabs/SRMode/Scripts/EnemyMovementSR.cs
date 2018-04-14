using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyMovementSR : EnemyMovement {

    private void FixedUpdate()
    {
        targetedPlayer = SharedMovement.SelectEnemy(gameObject, PlayerManager.Players.Where(t => t.gameObject.tag == gameObject.tag).ToList());
        navAgent.SetDestination(targetedPlayer.transform.position);
    }
}
