using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsanityPotion : MonoBehaviour {

	public int RewardInsanityAmount = 30;
	public float scaleTime = 0.3f;
	public float StartBlinkingAfterSeconds = 3;
	private float TimeSinceBlink = 0f;
	public float BlinkSpeed = 0.2f;
	public float RemoveAfterSeconds = 6;
	public SpriteRenderer Renderer;

	private bool finishedScaling = false;
	private Vector3 scale = Vector3.zero;
	private float timeInScene = 0f;

	[HideInInspector]
	public AudioManager audioManager;
	public AudioClip potionSound;
	public AudioClip potionDrop;

	private AudioSource audioSource;

	private bool hasBeenPickedUP = false;

	void Awake() 
	{
		audioSource = gameObject.GetComponent<AudioSource> ();
		audioManager = GameObject.Find ("AudioManager").GetComponent<AudioManager> ();
		audioManager.PlaySound (audioSource, potionDrop, 1.1f, true); 
	}

	private void Start()
	{
		scaleTime = 1 / scaleTime;
	}

	void Update()
	{
		if (hasBeenPickedUP)
		{
			Renderer.enabled = false;
			return;
		}
		timeInScene += Time.deltaTime;
		if (timeInScene > StartBlinkingAfterSeconds && timeInScene < RemoveAfterSeconds)
		{
			TimeSinceBlink += Time.deltaTime;
			if(TimeSinceBlink > BlinkSpeed)
			{
				TimeSinceBlink = 0;
				Renderer.enabled = !Renderer.enabled;
			}
		}
		if(timeInScene > RemoveAfterSeconds)
		{
			Destroy(gameObject);
		}
		if (finishedScaling)
		{
			return;
		}
		transform.localScale = scale;
		scale += Vector3.one * scaleTime * Time.deltaTime;
		if (scale.x > Vector3.one.x)
		{
			scale = Vector3.one;
			finishedScaling = true;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player" && !hasBeenPickedUP)
		{
			hasBeenPickedUP = true;
			other.GetComponent<IPlayer>().RewardSanity(RewardInsanityAmount);
			StartCoroutine ("PlayPotionSound");
		}
	}

	IEnumerator PlayPotionSound() {
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
		audioManager.PlaySound (audioSource, potionSound, 2f, true); 
		yield return new WaitForSeconds (potionSound.length);
		Destroy(gameObject);
	}
}
