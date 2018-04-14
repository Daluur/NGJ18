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
		audioSource.pitch = 1.08f;
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

	public IEnumerator ChangeBackgroundPitch(float startPitch, float targetPitch, float fadeSpeed) 
	{
		float t = 0;
		while (t < fadeSpeed) 
		{
			t += Time.deltaTime;
			float blend = Mathf.Clamp01(t / fadeSpeed);
			audioSource.pitch = Mathf.Lerp(startPitch, targetPitch, blend);
			yield return null;
		}
	}

}
