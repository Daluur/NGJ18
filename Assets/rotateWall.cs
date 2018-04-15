using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateWall : MonoBehaviour {

	public float speed = 2;
	void Start () {
		
	}

	void Update () {

		transform.Rotate(Vector3.up * Time.deltaTime *speed);
	}
}
