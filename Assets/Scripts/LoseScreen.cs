using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return))
		{
			Time.timeScale = 1f;
			SceneManager.LoadScene(0);
		}
		else if (Input.GetKeyDown(KeyCode.Space))
		{
			Time.timeScale = 1f;
			SceneManager.LoadScene(1);
		}
	}
}
