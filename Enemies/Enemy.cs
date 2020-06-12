using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {
    public float hitPoints;                     // Enemy current hit points. 
    public int damageStrenght;                  // How much damage the enemy causes to the player.
    public float recoil;                        // How much the enemy is pushed away when hit by the player's projectiles.
    public GameObject[] loot;                   // Loot to be dropped by the enemy.
    Coroutine damageCoroutine;                  // Reference to damage character coroutine.
    private Animation animation;                // Animation component reference.

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable() {
        // set starting hit points for this enemy.
        ResetCharacter();

        animation = GetComponent<Animation>();
    }

    /// <summary>
    /// Damage the enemy.
    /// </summary>
    /// <param name="damage">int - damage received by the character</param>
    /// <param name="interval">float - interval to use if the damage is recurrent</param>
    /// <param name="damager">Collider2D - used for applying logic based on collider.</param>
    /// <returns>IEnumerator</returns>
    public override IEnumerator DamageCharacter( int damage, float interval, Collider2D damager = null ) {

        while ( true ) {

            this.hitPoints = hitPoints - damage;

            // display damaged animation for enemy.
            if ( animation != null ) {
                Utils.instance.TriggerAnimation( animation, "damagedEnemy" );
            }

            // push enemy away when hit by projectile.
            if ( damager != null ) {
                // PushAway( damager );
            }

            if ( this.hitPoints <= float.Epsilon ) {
                KillCharacter();
                DropLoot();
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
        this.hitPoints = startingHitPoints;
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D( Collision2D collision ) {
        
        // check if the enemy is colliding the player.
        if ( collision.gameObject.CompareTag( "Player" ) ) {
            
            Player player = collision.gameObject.GetComponent<Player>();

            // init damage player.
            if ( damageCoroutine == null ) {
                damageCoroutine = StartCoroutine( player.DamageCharacter( damageStrenght, 1.0f ) );
            }
        }

    }


    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit2D( Collision2D collision ) {
        
        // check if the enemy stops colliding with the player.
        if ( collision.gameObject.CompareTag( "Player" ) ) {

            // check if the enemy was damaging the player before.
            if ( damageCoroutine != null ) {
                StopCoroutine( damageCoroutine );
                damageCoroutine = null;
            }
        } 
    }

    /// <summary>
    /// Potentially drops loot when the enemy is killed.
    /// </summary>
    private void DropLoot() {

        if ( loot.Length > 0 ) {
            int rand = (int) Mathf.Round( Random.Range( 0f, 10f ) );

            if ( rand <= loot.Length ) {
                int key = ( rand == 0 ) ? rand : rand - 1;
                Instantiate( loot[ key ], transform.position, Quaternion.identity );
            }
        }
        
    }

    /// <summary>
    /// Push enemy in the oposite direction
    /// when damaged by the player's projectiles.
    /// </summary>
    /// <param name="damager">Collider2D - collider which causes the damage to the enemy.</param>
    private void PushAway( Collider2D collider ) {
        var force = collider.transform.position + transform.position;

        force.Normalize();  
        GetComponent<Rigidbody2D>().AddForce( - ( ( force * this.damageStrenght ) * 600f ) );
    }


}
