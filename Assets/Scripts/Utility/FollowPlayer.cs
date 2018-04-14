using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour { 

	public GeneralPlayer player;
	private bool inMonsterMode = false;
	public GameObject MonsterParticles;
	public GameObject PlayerParticles;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (inMonsterMode)
		{
			if (!player.playerHealth.GetIsInsane()) {
				MonsterParticles.SetActive(false);
				// PlayerParticles.SetActive(true);
			}
		}
		else
		{
			if(player.playerHealth.GetIsInsane())
			{
				MonsterParticles.SetActive(true);
				// PlayerParticles.SetActive(false);
			}
		}
		transform.position = player.transform.position;
	}
}
