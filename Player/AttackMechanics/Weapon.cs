using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    
    public GameObject ammoPrefab;                       // Ammo prefab - used to instantiate in the object pool for ammo.
    public int poolSize;                                // Ammo object pool size.
    static List<GameObject> ammoPool;                   // Ammo pool list of gameoObjects.
    
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        SetAmmoObjectPool();
    }

    /// <summary>
    /// Set up ammo object pool.
    /// This is done to reuse ammo gameObjects
    /// and improve performance avoinding
    /// extra instatiation / destruction of ammo
    /// gameObjects.
    /// </summary>
    private void SetAmmoObjectPool() {

        if ( ammoPool == null ) {
            ammoPool = new List<GameObject>();
        }

        for ( int i = 0; i < poolSize; i++ ) {
            GameObject ammoObject = Instantiate( ammoPrefab );
            ammoObject.SetActive( false );
            ammoPool.Add( ammoObject );
        }
    }
}
