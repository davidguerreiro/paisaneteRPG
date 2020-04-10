using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {


    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D( Collider2D other ) {
        
        // check if the player is colliding with a consumable.
        if ( other.gameObject.CompareTag( "Collectible" )  ) {
            other.gameObject.SetActive( false );
        }
    }
}
