using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	private AudioSource audioSource;
	public AudioClip backgroundMusic;

	void Start () {
		audioSource = gameObject.GetComponent<AudioSource> ();
		audioSource.loop = true;
		audioSource.clip = backgroundMusic;
		audioSource.Play ();
	}

	void Update () {
	}
}
