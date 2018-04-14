using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsanityPotion : MonoBehaviour {

	public int RewardInsanityAmount = 30;
	public float scaleTime = 0.3f;

	private bool finishedScaling = false;
	private Vector3 scale = Vector3.zero;

	private void Start()
	{
		scaleTime = 1 / scaleTime;
	}

	void Update()
	{
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
		if(other.tag == "Player")
		{
			other.GetComponent<IPlayer>().RewardSanity(RewardInsanityAmount);
			Destroy(gameObject);
		}
	}
}
