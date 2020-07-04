using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGGameManager : MonoBehaviour {
    public static RPGGameManager instance;                                  // Public static class instance.
    public SpawnPoint playerSpawnPoint;                                     // Player spawn point class reference.
    public RPGCameraManager cameraManager;                                  // Camera manager

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        InitSingleton();
    }

    // Start is called before the first frame update
    void Start() {
        SetupScene();   
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        
        if ( Input.GetKey( "escape" ) ) {
            Application.Quit();
        }
    }

    /// <summary>
    /// Init singleton instance and
    /// avoid duplications of this singleton
    /// class.
    /// </summary>
    private void InitSingleton() {

        if ( instance != null && instance != this ) {
            Destroy( gameObject );
        } else {
            instance = this;
        }
    }

    /// <summary>
    /// Spawn the player.
    /// </summary>
    public void SpawnPlayer() {

        if ( playerSpawnPoint != null ) {
            GameObject player = playerSpawnPoint.SpawnObject();
            cameraManager.virtualCamera.Follow = player.transform;
        }
    }

    /// <summary>
    /// Setup instance logic.
    /// </summary>
    public void SetupScene() {

        // spawn the player once the scene is loaded.
        SpawnPlayer();
    }
}
