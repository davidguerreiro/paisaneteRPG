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
        if ( other.gameObject.CompareTag( "Collectible" ) ) {
            CheckCollectibleCollided( other.gameObject );
        }
    }

    /// <summary>
    /// Check collectible collided by
    /// the player and trigger game logic based
    /// on collectible item type.
    /// </summary>
    /// <param name="objectCollided">GameObject - object collided gameObject reference.</param>
    private void CheckCollectibleCollided( GameObject objectCollided ) {

        Item hitObject = objectCollided.GetComponent<Consumable>().item;

        if ( hitObject != null ) {

            // trigger logic based on item type.
            switch ( hitObject.itemType ) {
                case Item.ItemType.COIN:            // logic for coins
                    break;
                case Item.ItemType.HEALTH:
                    AdjustHitPoints( hitObject.quantity );
                    break;
                default:
                    break;
            }
        }

        objectCollided.SetActive( false );
    }

    /// <summary>
    /// Update player hit points.
    /// </summary>
    /// <param name="amount">int - amount of hit points to use to update player's healt</param>
    public void AdjustHitPoints( int amount ) {
        hitPoints = hitPoints + amount;
        Debug.Log( "Adjusted hitpoints by: " + amount + ". New value:" + hitPoints );
    }
}
