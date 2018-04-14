using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public Enemy[] EnemiesItCanSpawn;
    public Vector2Int[] Ranges;
    public float SpawnDelayForPS = 2f;
    public ParticleSystem spawnParticleSystem;

	public AudioManager audioManager;
	public AudioClip spawnBugSound;
	public AudioClip spawnBossSound;
	private AudioSource audioSource;

    private PlayerManager _playerManager;
    public PlayerManager PlayerManager
    {
        get
        {
            if (_playerManager == null)
            {
                _playerManager = GameObject.FindGameObjectWithTag("Playermanager").GetComponent<PlayerManager>();
            }
            return _playerManager;
        }
    }

	void Awake() 
	{
		audioSource = gameObject.GetComponent<AudioSource> ();
		audioManager = GameObject.Find ("AudioManager").GetComponent<AudioManager> ();
	} 

    public void StartSpawnSequence() {
		if (spawnParticleSystem != null) {
			spawnParticleSystem.Play ();
		}
        StartCoroutine(ShowParticleSystemBeforeSpawn());
    }

    private IEnumerator ShowParticleSystemBeforeSpawn() {
        var idToSpawn = Random.Range(0, EnemiesItCanSpawn.Length);
        var enemyToSpawn = EnemiesItCanSpawn[idToSpawn];
        var amountToSpawn = Random.Range(Ranges[idToSpawn].x, Ranges[idToSpawn].y + 1);
		if (!enemyToSpawn.DieOnPlayerHit && idToSpawn > 0) { // Means it is the boss.
			audioManager.PlaySound (audioSource, spawnBossSound, 1.8f, true);
		} else {
			audioManager.PlaySound (audioSource, spawnBugSound, 1f, true);
		}
		yield return new WaitForSeconds(SpawnDelayForPS);
        Spawner.ActualSpawnEnemy(gameObject.transform.position, PlayerManager.Players, enemyToSpawn, amountToSpawn);
    }
}
