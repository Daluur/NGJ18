using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float MoveSpeed = 3;
	public Transform GunModel;
	public PlayerShoot Gun;

	public int controllerID = 0;

	private string moveXString;
	private string moveYString;
	private string torsoXString;
	private string torsoYString;
	private string shootString;

	void Start()
	{
		if (controllerID == 0)
		{
			moveXString = "KeyboardMoveX";
			moveYString = "KeyboardMoveY";
			torsoXString = "KeyboardTorsoX";
			torsoYString = "KeyboardTorsoY";
			shootString = "KeyboardShoot";
		}
		else
		{
			moveXString = "Joy" + controllerID + "MoveX";
			moveYString = "Joy" + controllerID + "MoveY";
			torsoXString = "Joy" + controllerID + "TorsoX";
			torsoYString = "Joy" + controllerID + "TorsoY";
			shootString = "Joy" + controllerID + "Shoot";
		}
	}

	// Update is called once per frame
	void Update () {
		var xAxis = Input.GetAxis(moveXString) * MoveSpeed * Time.deltaTime;
		var yAxis = Input.GetAxis(moveYString) * MoveSpeed * Time.deltaTime;

		transform.position += new Vector3(xAxis, 0, yAxis);

		var torsoX = Input.GetAxis(torsoXString);
		var torsoY = Input.GetAxis(torsoYString);

		if (torsoX + torsoY != 0)
		{
			GunModel.eulerAngles = new Vector3(GunModel.eulerAngles.x, Mathf.Atan2(torsoX, torsoY) * Mathf.Rad2Deg, GunModel.eulerAngles.z);
		}

		if (Input.GetButtonDown(shootString))
		{
			Gun.Shoot();
		}
	}
}
