using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimExplosion : MonoBehaviour {

    public ParticleSystem ps;

	public AudioClip audioClip;

	[HideInInspector]
	public AudioManager audioManager;
	private AudioSource audioScource;

	void Awake()
	{
		audioScource = gameObject.GetComponent<AudioSource> ();
		audioManager = GameObject.Find ("AudioManager").GetComponent<AudioManager> ();
	} 

	void Start () {
		if (audioClip != null)
		{
			audioManager.PlaySound(audioScource, audioClip, 1f, true);
		}
        StartCoroutine(KillAfterPSFinishes(ps.main.duration));
        ps.Play();  	
	}

    private IEnumerator KillAfterPSFinishes(float timeOfPS)
    {
        yield return new WaitForSeconds(timeOfPS + 0.3f);
        Destroy(gameObject);
    }
}
