using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour {

	public GameObject KeyboardText;
	public GameObject ControllerText;

	private void Start()
	{
		if(CrossSceneData.Instance.GetPlayers() == null)
		{
			ControllerText.SetActive(false);
			KeyboardText.SetActive(true);
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("JoyStart"))
		{
			var numberOfLevels = SceneManager.sceneCountInBuildSettings - 3; // We do not count main menu or credits.
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
