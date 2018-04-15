using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("JoySelect"))
		{
			// Time.timeScale = 1f;
			EnemyMovement.GameEnded = false;
			PlayerController.GameEnded = false;
			Spawner.GameEnded = false;
			PlayerHealth.GameEnded = false;
			SanityBar.GameEnded = false;
			SpawnPoint.GameEnded = false;
			SceneManager.LoadScene(0);
		}
		else if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("JoyStart"))
		{
			// Time.timeScale = 1f;
			EnemyMovement.GameEnded = false;
			PlayerController.GameEnded = false;
			Spawner.GameEnded = false;
			PlayerHealth.GameEnded = false;
			SanityBar.GameEnded = false;
			SpawnPoint.GameEnded = false;
			var numberOfLevels = SceneManager.sceneCountInBuildSettings - 2; // We do not count main menu or credits.
			var levelToLoad = Random.Range(1, numberOfLevels + 1);
			if (numberOfLevels > 1 && levelToLoad == SceneManager.GetActiveScene().buildIndex)
			{
				levelToLoad = Random.Range(1, numberOfLevels + 1);
				if (numberOfLevels > 1 && levelToLoad == SceneManager.GetActiveScene().buildIndex)
				{
					levelToLoad = Random.Range(1, numberOfLevels + 1);
				}
			}
			SceneManager.LoadScene(levelToLoad);
		}
	}
}
