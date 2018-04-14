using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingWall : MonoBehaviour {

	public Vector3 pos1 = new Vector3(0,0,1.25f);
	public Vector3 pos2 = new Vector3(0,0,-3.8f);
	public float speed = 0.1f;

	void Update() {
		transform.position = Vector3.Lerp (pos1, pos2, Mathf.PingPong(Time.time*speed, 1.0f));
	}
}
