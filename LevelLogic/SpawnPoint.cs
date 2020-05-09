using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public GameObject prefabToSpawn;                                // Prefab spawn type to instantiate in this spawn point.
    public float repeatInterval;                                    // Interval to when to respawn item again.

    // Start is called before the first frame update
    void Start() {
        SpawnByInterval();
    }

    /// <summary>
    /// Spawn gameObject prefab.
    /// </summary>
    /// <returns>GameObject</returns>
    public GameObject SpawnObject() {

        if ( prefabToSpawn != null ) {
            return Instantiate( prefabToSpawn, transform.position, Quaternion.identity );
        }

        return null;
    }

    /// <summary>
    /// Spawn by interval.
    /// </summary>
    private void SpawnByInterval() {

        if ( repeatInterval > 0f ) {
            InvokeRepeating( "SpawnObject", 0.0f, repeatInterval );
        }
    }
}
