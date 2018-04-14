using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	private AudioSource audioSource;
	public AudioClip backgroundMusic;

	void Start () 
	{
		audioSource = gameObject.GetComponent<AudioSource> ();
		audioSource.loop = true;
		audioSource.clip = backgroundMusic;
		audioSource.Play ();
	}

	public void PlayRandomSound(AudioSource source, AudioClip[] clips)
	{
		int rnd = Random.Range (0, clips.Length);
		source.clip = clips[rnd];
		source.pitch = Random.Range (.9f, 1.1f);
		source.volume = Random.Range (.9f, 1.1f);
		source.Play ();
	}

	public void PlaySound(AudioSource source, AudioClip clip)
	{
		source.clip = clip;
		source.pitch = Random.Range (.9f, 1.1f);
		source.volume = Random.Range (.9f, 1.1f);
		source.Play ();
	}
}
