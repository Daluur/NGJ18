using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spawner : MonoBehaviour {

    public float SpawnInterval = 1;
    public float SpawnIntervalDecrease = 0.05f;
    public float TimeBeforeSpawnIntervalDecreases = 30f;

    public GameObject[] SpawnPoints;
    public GameObject EnemyPrefab;


	// Use this for initialization
	void Start () {
        StartEnemySpawning();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void FixedUpdate()
    {

    }

    private void StartEnemySpawning() {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnIntervalDecreaseCoroutine());
    }

    private IEnumerator SpawnIntervalDecreaseCoroutine() {
		yield return new WaitForSeconds(3);
		while (true) {
            yield return new WaitForSeconds(TimeBeforeSpawnIntervalDecreases);
            if(SpawnInterval - SpawnIntervalDecrease > 0.015f)
            {
                SpawnInterval -= SpawnIntervalDecrease;
            }
        }
    }
    
    private IEnumerator SpawnEnemies() {
		yield return new WaitForSeconds(3);
        while (true)
        {
            yield return new WaitForSeconds(SpawnInterval);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy() {
        var spawnPointIdx = Random.Range(0, SpawnPoints.Length + 1);
        if(spawnPointIdx == SpawnPoints.Length) {
            foreach(var sp in SpawnPoints)
            {
                ActualSpawnEnemy(sp.transform.position);
            }
        }
        else {
            ActualSpawnEnemy(SpawnPoints[spawnPointIdx].transform.position);
        }
    }

    private void ActualSpawnEnemy(Vector3 spawnPos)
	{
        var go = Instantiate(EnemyPrefab, spawnPos, Quaternion.identity);
        go.GetComponent<EnemyMovement>().Players = PlayerManager.Instance.Players.Select(t => t.gameObject).ToArray();
    }


}
