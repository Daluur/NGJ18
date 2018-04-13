using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimExplosion : MonoBehaviour {

    public ParticleSystem ps;

	void Start () {
        StartCoroutine(KillAfterPSFinishes(ps.time));
        ps.Play();  	
	}

    private IEnumerator KillAfterPSFinishes(float timeOfPS)
    {
        yield return new WaitForSeconds(timeOfPS + 0.3f);
        Destroy(gameObject);
    }
}
