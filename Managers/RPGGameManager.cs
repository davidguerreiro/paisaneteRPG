using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGGameManager : MonoBehaviour {

    public static RPGGameManager instance;                                  // Public static class instance.

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
    /// Setup instance logic.
    /// </summary>
    public void SetupScene() {

    }
}
