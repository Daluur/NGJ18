using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

	public Transform BulletSpawnPosition;
	public GameObject[] Bullets;
    public PlayerHealth playerHealth;
	public float FireRate = 0.3f;
	private float timeSinceLastFire = Mathf.Infinity;
	private int lastBulletTypeFired = 0;
	private int amountOfBullets;

    public LineRenderer Laser;
    public LayerMask LaserLayerMask;
    public float LaserDamagerPerSecond = 20;
    public CapsuleCollider LaserCollider;

	public AudioManager audioManager;
	public AudioClip[] shootingSound;

	private AudioSource audioSource;

	void Awake() 
	{
		audioSource = gameObject.GetComponent<AudioSource> ();
		audioManager = GameObject.Find ("AudioManager").GetComponent<AudioManager> ();
	} 

	private void Start()
	{
		amountOfBullets = Bullets.Length - 1;
	}

	public void Shoot()
	{
        if (playerHealth.GodMode)
        {
            LaserCollider.gameObject.SetActive(true);
            RaycastHit hit;
            if(Physics.Raycast(BulletSpawnPosition.position, BulletSpawnPosition.forward, out hit, 1000f, LaserLayerMask))
            {
                LaserCollider.transform.localScale = new Vector3(hit.distance, 1f, 1f);
                LaserCollider.transform.position = BulletSpawnPosition.position + BulletSpawnPosition.forward * hit.distance / 2;
            }
            else {
                LaserCollider.transform.localScale = new Vector3(100f, 1f, 1f);
                LaserCollider.transform.position = BulletSpawnPosition.position + BulletSpawnPosition.forward * 100f / 2;
            }

            LaserCollider.transform.position += new Vector3(0f, 0.2f, 0f);
            
            return;
        }

		if(timeSinceLastFire < FireRate)
		{
			return;
		}
		timeSinceLastFire = 0;

		var bullet = Instantiate(Bullets[lastBulletTypeFired++], BulletSpawnPosition.position, transform.rotation);
		var direction = BulletSpawnPosition.forward;
		direction.y = 0;
		bullet.GetComponent<Bullet>().Setup(direction, playerHealth);
		audioManager.PlayRandomSound (audioSource, shootingSound); 
		if(lastBulletTypeFired > amountOfBullets)
		{
			lastBulletTypeFired = 0;
		}
	}

	private void Update()
	{
        if (!playerHealth.playerController.isShooting)
        {
            LaserCollider.gameObject.SetActive(false);
            return;
        }
        timeSinceLastFire += Time.deltaTime;
	}
}
