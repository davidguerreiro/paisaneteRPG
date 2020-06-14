using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {
    public HitPoints hitPoints;                                     // Current hit points value.
    public PlayerAmmo playerAmmo;                                   // Current player ammo.
    public HealthBar healthBarPrefab;                               // Reference to health bar prefab class. Used her to instantiate a copy.
    public Inventory inventory;                                     // This stores a reference to the inventory prefab copy.
    private HealthBar healthBar;                                    // This stores the refernce to the healthbar prefab copy.
    private Animation animation;                                    // Animation component reference.
    private AudioComponent audio;                                   // Audio component class reference.

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        ResetCharacter();
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
                    shouldDissapear = AdjustHitPoints( hitObject.quantity );
                    break;
                case Item.ItemType.AMMO:
                    shouldDissapear = AdjustAmmo( hitObject.quantity );
                    break;
                default:
                    break;
            }

            if ( shouldDissapear ) {
                objectCollided.SetActive( false );

                 // play collectible sound.
                 if ( hitObject.collectableSound != null ) {
                     audio.PlayClip( hitObject.collectableSound );
                 }
            }
        }

        
    }

    /// <summary>
    /// Update player hit points.
    /// </summary>
    /// <param name="amount">int - amount of hit points to use to update player's healt</param>
    /// <returns>bool</returns>
    public bool AdjustHitPoints( int amount ) {

        // check that the player can collect the hearth
        if ( hitPoints.value < maxHitPoints ) {

            hitPoints.value = hitPoints.value + amount;
            // Debug.Log( "Adjusted hitpoints by: " + amount + ". New value:" + hitPoints.value );    

            return true;
        }
        
        return false;
    }

    /// <summary>
    /// Adjust player ammo.
    /// </summary>
    /// <param name="amount">int - amount of ammo updated</param>
    /// <returns>bool</returns>
    public bool AdjustAmmo( int amount ) {

        // check if the player can carry more munition.
        if ( playerAmmo.value < playerAmmo.maximun ) {

            playerAmmo.value += amount;

            if ( playerAmmo.value > playerAmmo.maximun ) {
                playerAmmo.value = playerAmmo.maximun;
                return false;
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// Kill character.
    /// </summary>
    public override void KillCharacter() {

        // call parent killCharacter.
        base.KillCharacter();

        // remove current game UI.
        Destroy( healthBar.gameObject );
        Destroy( inventory.gameObject );
    }

    /// <summary>
    /// Damage character.
    /// </summary>
    /// <param name="damage">int - damage received by the character</param>
    /// <param name="interval">float - interval to use if the damage is recurrent</param>
    /// <param name="damager">Collider2D - used for applying logic based on collider.</param>
    /// <returns>IEnumerator</returns>
    public override IEnumerator DamageCharacter( int damage, float interval, Collider2D damager = null ) {

        while ( true ) {

            this.hitPoints.value = hitPoints.value - damage;

            // display hurt animation.
            if ( animation != null ) {
                Utils.instance.TriggerAnimation( animation, "damagedEnemy" );
            }

            if ( this.hitPoints.value <= float.Epsilon ) {
                KillCharacter();
                break;
            }

            // check if recurrent damage.
            if ( interval > float.Epsilon ) {
                yield return new WaitForSeconds( interval );
            } else {
                break;
            }
        }
    }

    /// <summary>
    /// Reset Character.
    /// </summary>
    public override void ResetCharacter() {

        // set default initial starting hitpoints.
        hitPoints.value = startingHitPoints;
        
        // set healthbar script player referece to this instance.
        // healthBar.character = this;`

        // instantiate a copy of the inventory.
        // inventory = Instantiate( inventoryPrefab );

        // health bar and inventory setup when the player is instantiated.
        // setup healthbar.
        healthBar = GameObject.FindGameObjectWithTag( "HealthBar" ).GetComponent<HealthBar>();
        healthBar.SetPlayerReference( this );

        // setup inventory.
        inventory = GameObject.FindGameObjectWithTag( "Inventory" ).GetComponent<Inventory>();

        // get animation component.
        animation = GetComponent<Animation>();

        // get audio componenent class refernece.
        audio = GetComponent<AudioComponent>();
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

    }
}
