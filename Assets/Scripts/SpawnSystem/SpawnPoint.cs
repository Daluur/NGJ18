using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public Enemy[] EnemiesItCanSpawn;
    public Vector2Int[] Ranges;
    public float SpawnDelayForPS = 2f;
    public ParticleSystem spawnParticleSystem;

    public void StartSpawnSequence() {
        if(spawnParticleSystem!=null)
            spawnParticleSystem.Play();
        StartCoroutine(ShowParticleSystemBeforeSpawn());
    }

    private IEnumerator ShowParticleSystemBeforeSpawn() {
        Debug.Log(gameObject.name);
        yield return new WaitForSeconds(SpawnDelayForPS);
        var idToSpawn = Random.Range(0, EnemiesItCanSpawn.Length);
        var enemyToSpawn = EnemiesItCanSpawn[idToSpawn];
        var amountToSpawn = Random.Range(Ranges[idToSpawn].x, Ranges[idToSpawn].y);
        Spawner.ActualSpawnEnemy(gameObject.transform.position, enemyToSpawn, amountToSpawn);
    }
}
