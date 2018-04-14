using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SharedMovement : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public static GameObject SelectEnemy(GameObject mover, List<GeneralPlayer> Players)
    {
        var targets = Players;
        var enemyPos = new Vector2(mover.transform.position.x, mover.transform.position.z);
        float closesDist = float.MaxValue, dist;
        GameObject closestPlayer = targets[0].gameObject;
        foreach (var player in targets)
        {
            if (player.playerHealth.IsInsane) {
                continue;
            }
            dist = Vector2.Distance(enemyPos, new Vector2(player.gameObject.transform.position.x, player.gameObject.transform.position.z));
            if (Mathf.Abs(dist) < closesDist)
            {
                closestPlayer = player.gameObject;
                closesDist = dist;
            }
        }
        return closestPlayer;
    }

}
