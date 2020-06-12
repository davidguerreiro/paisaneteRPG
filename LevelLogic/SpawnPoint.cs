using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    // Defines list spawn type.
    public enum SpawnPointType{
        player,
        enemies,
    };

    public SpawnPointType spawnPointType;                           // Spwan point type.
    public GameObject prefabToSpawn;                                // Prefab spawn type to instantiate in this spawn point.
    public float repeatInterval;                                    // Interval to when to respawn item again.
    public bool canSpawn;                                           // Defines whether this spwan point can spwan entities into the game.
    private string spawnType;                                         // Spawn point type.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Spawn gameObject prefab.
    /// </summary>
    /// <returns>GameObject</returns>
    public GameObject SpawnObject() {

        if ( prefabToSpawn != null && canSpawn ) {
            return Instantiate( prefabToSpawn, transform.position, Quaternion.identity );
        }

        return null;
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D( Collider2D other ) {
        
        // spawn enemies if player enters spawn area.
        if ( spawnType == "enemies" && other.gameObject.CompareTag( "Player" ) ) {
            this.canSpawn = true;
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D( Collider2D other ) {
        
        // stop spawning enemies if the player leaves the spawning area.
        if ( spawnType == "enemies" && other.gameObject.CompareTag( "Player" ) ) {
            this.canSpawn = false;
        }
    }

    /// <summary>
    /// Spawn by interval.
    /// </summary>
    private void SpawnByInterval() {

        if ( repeatInterval > 0f ) {
            InvokeRepeating( "SpawnObject", 0.0f, repeatInterval );
        }
    }

    /// <summary>
    /// Set spawn point type
    /// from public enum list.
    /// </summary>
    private void GetSpawnPointValue() {
        switch ( spawnPointType ) {
            case SpawnPointType.player:
                this.spawnType = "player";
                break;
            case SpawnPointType.enemies:
                this.spawnType = "enemies";
                break;
            default:
                this.spawnType = "player";
                break;
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // set spawn point type from editor.
        GetSpawnPointValue();

        // start spawning stuff.
        SpawnByInterval();
    }
}
