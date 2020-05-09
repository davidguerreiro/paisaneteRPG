using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RPGCameraManager : MonoBehaviour {

    public static RPGCameraManager instance = null;                     // Public static class instance.
    [HideInInspector]
    public CinemachineVirtualCamera virtualCamera;                      // Virtual Camera reference.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        
        if ( instance != null && instance != this ) {
            Destroy( gameObject );
        } else {
            instance = this;
        }

        // get virtual camera.
        virtualCamera = GameObject.FindWithTag( "VirtualCamera" ).GetComponent<CinemachineVirtualCamera>();
    }
    
}
