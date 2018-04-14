using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnerSR : Spawner {

    private SpawnGroup[] spawnGroupsTeam1;
    private SpawnGroup[] spawnGroupsTeam2;

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

    private List<GeneralPlayer> team1;
    private List<GeneralPlayer> team2;

    void Start() {
        spawnGroupsTeam1 = SpawnGroups.Where(t => t.gameObject.tag == "Team1").ToArray();
        spawnGroupsTeam2 = SpawnGroups.Where(t => t.gameObject.tag == "Team2").ToArray();
        team1 = PlayerManager.Players.Where(t => t.gameObject.tag == "Team1").ToList();
        team2 = PlayerManager.Players.Where(t => t.gameObject.tag == "Team2").ToList();
        var spawnGroup = Random.Range(0, spawnGroupsTeam2.Length);
        Spawner.ActualSpawnEnemy(spawnGroupsTeam2[spawnGroup].transform.position, team2, spawnGroupsTeam2[spawnGroup].SpawnPoints.FirstOrDefault().EnemiesItCanSpawn.FirstOrDefault());
        Spawner.ActualSpawnEnemy(spawnGroupsTeam1[spawnGroup].transform.position, team1, spawnGroupsTeam1[spawnGroup].SpawnPoints.FirstOrDefault().EnemiesItCanSpawn.FirstOrDefault());
    }

    public void BugDied(int team) {
        for (int i = 0; i < 2; i++) { 
            if (team == 1)
            {
                var spawnGroup = Random.Range(0, spawnGroupsTeam2.Length);
                Spawner.ActualSpawnEnemy(spawnGroupsTeam2[spawnGroup].transform.position, team2, spawnGroupsTeam2[spawnGroup].SpawnPoints.FirstOrDefault().EnemiesItCanSpawn.FirstOrDefault());
            }
            else
            {
                var spawnGroup = Random.Range(0, spawnGroupsTeam1.Length);
                Spawner.ActualSpawnEnemy(spawnGroupsTeam1[spawnGroup].transform.position, team1, spawnGroupsTeam1[spawnGroup].SpawnPoints.FirstOrDefault().EnemiesItCanSpawn.FirstOrDefault());
            }
        }
    }



}
