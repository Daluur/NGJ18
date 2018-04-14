using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySR : EnemyHealth {

    private SpawnerSR spawner;
    public int Team;

    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("SpawnerSR").GetComponent<SpawnerSR>();
    }

    private void OnDestroy()
    {
        spawner.BugDied(Team);
    }

}
