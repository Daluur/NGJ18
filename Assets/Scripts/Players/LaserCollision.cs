using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollision : MonoBehaviour {

    public PlayerShoot PlayerShoot;
    public PlayerController PlayerController;
    private float accDmg = 0;
    private int wholeNumberDmg = 0;

	[HideInInspector]
	public AudioManager audioManager;
	public AudioClip laserSound;

	private AudioSource audioSource;

	void Awake() 
	{
		audioSource = gameObject.GetComponent<AudioSource> ();
		audioManager = GameObject.Find ("AudioManager").GetComponent<AudioManager> ();
	} 
	
	// Update is called once per frame
	void Update () {
        if (PlayerController.isShooting)
        {
			if (!audioSource.isPlaying) {
				audioManager.PlaySound (audioSource, laserSound, .8f, false);
			} 
            accDmg += PlayerShoot.LaserDamagerPerSecond * Time.deltaTime;
            if(accDmg >= 1)
            {
                wholeNumberDmg = Mathf.FloorToInt(accDmg);
                accDmg -= wholeNumberDmg;
            }
		} else {
			audioSource.Stop ();
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
