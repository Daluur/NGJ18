using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("JoySelect"))
		{
			SceneManager.LoadScene(0);
		}
		else if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("JoyStart"))
		{
			SceneManager.LoadScene(1);
		}
	}
}
