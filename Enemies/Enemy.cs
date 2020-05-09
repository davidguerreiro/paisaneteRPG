using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    float hitPoints;                            // Enemy current hit points. 
    public int damageStrenght;                  // How much damage the enemy causes to the player.
    Coroutine damageCoroutine;                  // Reference to damage character coroutine.

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable() {
        // set starting hit points for this enemy.
        ResetCharacter();
    }

    /// <summary>
    /// Damage character.
    /// </summary>
    /// <param name="damage">int - damage received by the character</param>
    /// <param name="interval">float - interval to use if the damage is recurrent</param>
    /// <returns>IEnumerator</returns>
    public override IEnumerator DamageCharacter( int damage, float interval ) {

        while ( true ) {

            this.hitPoints = hitPoints - damage;

            if ( this.hitPoints <= float.Epsilon ) {
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

}
