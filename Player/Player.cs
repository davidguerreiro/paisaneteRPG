using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    public HealthBar healthBarPrefab;                       // Reference to health bar prefab class. Used her to instantiate a copy.
    public Inventory inventory;                                    // This stores a reference to the inventory prefab copy.
    private HealthBar healthBar;                                    // This stores the refernce to the healthbar prefab copy.

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        Init();
    }

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
            
            bool shouldDissapear = true;

            // trigger logic based on item type.
            switch ( hitObject.itemType ) {
                case Item.ItemType.COIN:            // logic for coins
                    shouldDissapear = inventory.AddItem( hitObject );
                    break;
                case Item.ItemType.HEALTH:
                    AdjustHitPoints( hitObject.quantity );
                    break;
                default:
                    break;
            }

            if ( shouldDissapear ) {
                objectCollided.SetActive( false );
            }
        }

        
    }

    /// <summary>
    /// Update player hit points.
    /// </summary>
    /// <param name="amount">int - amount of hit points to use to update player's healt</param>
    public bool AdjustHitPoints( int amount ) {

        // check that the player can collect the hearth
        if ( hitPoints.value < maxHitPoints ) {

            hitPoints.value = hitPoints.value + amount;
            Debug.Log( "Adjusted hitpoints by: " + amount + ". New value:" + hitPoints );    

            return true;
        }
        
        return false;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        Debug.Log( "called player" );

        // set default initial starting hitpoints.
        hitPoints.value = startingHitPoints;

        // instantiate a copy of the health bar prefab.
        healthBar = Instantiate( healthBarPrefab );

        // set healthbar script player referece to this instance.
        // healthBar.character = this;

        // instantiate a copy of the inventory.
        // inventory = Instantiate( inventoryPrefab );
    }
}
