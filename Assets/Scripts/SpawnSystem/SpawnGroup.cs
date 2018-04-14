using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGroup : MonoBehaviour {
    public SpawnPoint[] SpawnPoints;

    public void Spawn() {
        foreach(var sp in SpawnPoints)
        {
            sp.StartSpawnSequence();
        }
    }
}
