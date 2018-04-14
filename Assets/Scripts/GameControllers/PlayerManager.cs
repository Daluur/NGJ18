using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager> {

    public GameObject[] Players;

    public int DepletePerTick = 4;
    public int MaxDepletePerSecond = 8;
    public int DepleteIncreaseInterval = 60;

    public float DepleteInterval = 1;

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
