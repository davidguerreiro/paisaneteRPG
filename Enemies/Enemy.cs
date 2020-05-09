using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    float hitPoints;                            // Enemy current hit points. 

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


}
