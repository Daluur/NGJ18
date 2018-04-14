using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("JoySelect"))
		{
			Time.timeScale = 1f;
			SceneManager.LoadScene(0);
		}
		else if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("JoyStart"))
		{
			Time.timeScale = 1f;
			var numberOfLevels = SceneManager.sceneCountInBuildSettings - 2; // We do not count main menu or credits.
			var levelToLoad = Random.Range(1, numberOfLevels + 1);
			SceneManager.LoadScene(levelToLoad);
		}
	}
}
