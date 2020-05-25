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
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        
        if ( Input.GetMouseButtonDown(0) ) {
            FireAmmo();
        }
    }

    /// <summary>
    /// Spawn ammo method.
    /// Spawn a new ammo proyectile by
    /// location.
    /// </summary>
    /// <parma name="location">Vector3 - where to spawn the ammo</param>
    /// <returns>GameObject</returns>
    private GameObject SpawnAmmo( Vector3 location ) {
        
        // loop through the ammo pool.
        foreach ( GameObject ammo in ammoPool ) {

            // if an unused disabled ammo gameobject is found in the pool, the use it.
            if ( ammo.activeSelf == false ) {

                ammo.SetActive( true );
                ammo.transform.position = location;

                return ammo;
            }
        }

        return null;
    }

    /// <summary>
    /// Fire ammo when the 
    /// player press attack button.
    /// </summary>
    private void FireAmmo() {

    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy() {
        ammoPool = null;
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

        for ( int i = 0; i < this.poolSize; i++ ) {
            GameObject ammoObject = Instantiate( ammoPrefab );
            ammoObject.SetActive( false );
            ammoPool.Add( ammoObject );
        }
    }
}
