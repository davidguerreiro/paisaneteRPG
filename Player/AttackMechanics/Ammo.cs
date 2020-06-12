using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

    public int damageInflicted;                             // Damage caused by this type of ammo.

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="collision">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D( Collider2D collision ) {
        
        // check if enemy has been impacted by proyectile.
        if ( collision.gameObject.CompareTag( "Enemy" ) && collision is BoxCollider2D ) {

            // damage enemy.
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            StartCoroutine( enemy.DamageCharacter( damageInflicted, 0.0f, collision ) );

            // remove proyectile.
            gameObject.SetActive( false );
        }
    }
}
