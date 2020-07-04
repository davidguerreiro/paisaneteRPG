using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract class means it cannot be instantiated, and therefore must be inherited by a subclass.
public abstract class Character : MonoBehaviour {
    public float maxHitPoints;                                      // Maximun hit points value.
    public float startingHitPoints;                                 // Number of hit points the character starts with.

    // Start is called before the first frame update
    void Start() {
            
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// <summary>
    /// Kill character.
    /// </summary>
    public virtual void KillCharacter() {
        Destroy( gameObject );
    }

    /// <summary>
    /// Display animation for character when is hurt.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public virtual IEnumerator FlickerCharacter() {

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        renderer.color = Color.red;
        yield return new WaitForSeconds( .1f );

        renderer.color = Color.white;
    }

    /// <summary>
    /// Reset Character.
    /// </summary>
    public abstract void ResetCharacter();

    /// <summary>
    /// Damage character.
    /// </summary>
    /// <param name="damage">int - damage received by the character</param>
    /// <param name="interval">float - interval to use if the damage is recurrent</param>
    /// <param name="damager">Collider2D - used for applying logic based on collider.</param>
    /// <returns>IEnumerator</returns>
    public abstract IEnumerator DamageCharacter( int damage, float interval, Collider2D damager = null ); 

}
