using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spawner : MonoBehaviour {

    public float SpawnIntervalOld = 1;
    public float SpawnIntervalDecreaseOld = 0.05f;
    public float TimeBeforeSpawnIntervalDecreasesOld = 30f;

    public float SpawnIntervalBetweenSequence = 1f;
    public float SpawnIntervalDecreaseSequence = 0.05f;
    public float TimeBeforeSpawnIntervalDecreasesSequence = 30f;

    public static int IncreaseInSpawn = 0;
    public float TimeBeforeSpawnIncreases = 60f;

    public SpawnGroup[] SpawnGroups;
    public GameObject EnemyPrefab;

    [Tooltip("Leaving an entry empty, means ALL SPAWNS!!!")]
    public SpawnGroup[] SequenceToSpawnFrom;

    [Tooltip("This uses the first way of spawning, it does not balance anything at all!")]
    public bool FullRandomSpawn = true;



	// Use this for initialization
	void Start () {
		IncreaseInSpawn = 0;
        if (FullRandomSpawn)
        {
            StartEnemySpawningOld();
        }
        else
        {
            StartCoroutine(SpawnIntervalDecreaseCoroutineSequence());
            StartCoroutine(SpawnEnemiesSequence());
            StartCoroutine(SpawnAmountIncreaseSequence());
        }

    }

    private IEnumerator SpawnAmountIncreaseSequence() {
        while (true)
        {
            yield return new WaitForSeconds(TimeBeforeSpawnIncreases);
            IncreaseInSpawn++;
        }
    }

    private IEnumerator SpawnIntervalDecreaseCoroutineSequence()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeBeforeSpawnIntervalDecreasesSequence);
            if (SpawnIntervalBetweenSequence - SpawnIntervalDecreaseSequence > 0.015f)
            {
                SpawnIntervalBetweenSequence -= SpawnIntervalDecreaseSequence;
            }
        }
    }

    private IEnumerator SpawnEnemiesSequence() {
        while (true)
        {
            foreach(var sp in SequenceToSpawnFrom) {
                yield return new WaitForSeconds(SpawnIntervalBetweenSequence);
                if (sp == null)
                {
                    foreach(var spawnPoint in SequenceToSpawnFrom)
                    {
                        sp.Spawn();
                    }
                }
                else
                { 
                    sp.Spawn();
                }
            }

        }
    }

    private void StartEnemySpawningOld() {
        StartCoroutine(SpawnEnemiesOld());
        StartCoroutine(SpawnIntervalDecreaseCoroutineOld());
    }

    private IEnumerator SpawnIntervalDecreaseCoroutineOld() {
		yield return new WaitForSeconds(3);
		while (true) {
            yield return new WaitForSeconds(TimeBeforeSpawnIntervalDecreasesOld);
            if(SpawnIntervalOld - SpawnIntervalDecreaseOld > 0.015f)
            {
                SpawnIntervalOld -= SpawnIntervalDecreaseOld;
            }
        }
    }
    
    private IEnumerator SpawnEnemiesOld() {
		yield return new WaitForSeconds(3);
        while (true)
        {
            yield return new WaitForSeconds(SpawnIntervalOld);
            SpawnEnemyOld();
        }
    }

    private void SpawnEnemyOld() {
        var spawnPointIdx = Random.Range(0, SpawnGroups.Length + 1);
        if(spawnPointIdx == SpawnGroups.Length) {
            foreach(var sp in SpawnGroups)
            {
                sp.Spawn();
            }
        }
        else {
            SpawnGroups[spawnPointIdx].Spawn();
        }
    }

    public static void ActualSpawnEnemy(Vector3 spawnPos, List<GeneralPlayer> players, Enemy toSpawn = null, int amountToSpawn = 1)
	{
		var amountToSpawnWithIncrease = amountToSpawn + Spawner.IncreaseInSpawn;
		if (!toSpawn.DieOnPlayerHit) // It is the boss.
		{
			// We make sure there can never spawn more than 1 boss at a time.
			amountToSpawnWithIncrease = Mathf.Min(amountToSpawnWithIncrease, 1);
		}

		for (int i = 0; i < amountToSpawn; i++)
        {
            var go = Instantiate(toSpawn.gameObject, spawnPos, Quaternion.identity);
            go.GetComponent<EnemyMovement>().Players = players.Select(t => t.gameObject).ToArray();
        }
    }

    public void OnDestroy()
    {
        StopAllCoroutines();
    }

}
