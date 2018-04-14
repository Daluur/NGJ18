using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollision : MonoBehaviour {

    public PlayerShoot PlayerShoot;
    public PlayerController PlayerController;
    private float accDmg = 0;
    private int wholeNumberDmg = 0;

	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerController.isShooting)
        {
            accDmg += PlayerShoot.LaserDamagerPerSecond * Time.deltaTime;
            if(accDmg >= 1)
            {
                wholeNumberDmg = Mathf.FloorToInt(accDmg);
                accDmg -= wholeNumberDmg;
            }
        }
	}

    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Enemy")
        {
			if (other.GetComponent<IEnemy>().IsBoss()){
				other.GetComponent<IEnemy>().TakeDamage(wholeNumberDmg * 10, PlayerShoot.playerHealth);
			}
			other.GetComponent<IEnemy>().TakeDamage(wholeNumberDmg * 100, PlayerShoot.playerHealth);
		}
        if(other.tag == "Player" && other.gameObject != PlayerShoot.playerHealth.gameObject)
        {
            other.GetComponent<IPlayer>().TakeDamage(wholeNumberDmg);
        }
    }
}
