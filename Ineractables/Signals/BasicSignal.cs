using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSignal : MonoBehaviour {
    private AudioComponent audio;                                   // Audio component class reference.
    private bool collided;                                          // Flag to control the player has not collided before. We let player to collide once to read the signal, after that, we play collision sfx.
    private bool playingAudio;                                      // Flag to control collison audio is not played several times at the same time.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Play collision audio.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator PlayCollisionAudio() {
        this.playingAudio = true;

        audio.PlaySound( 0 );
        yield return new WaitForSeconds( 1f );

        this.playingAudio = false;
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D( Collision2D other ) {

        // trigger logic only with player stops colliding this object.
        if ( other.gameObject.tag == "Player" ) {
            
            if ( this.collided && ! this.playingAudio ) {
                StartCoroutine( PlayCollisionAudio() );
            }

            this.collided = true;
        }
        
    }

    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit2D( Collision2D other ) {
        
        // trigger logic only when the player stops colliding this object.
        if ( other.gameObject.tag == "Player" ) {
            this.collided = false;
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get audio component reference.
        audio = GetComponent<AudioComponent>();

        // set default value for control flags.
        this.collided = false;
        this.playingAudio = false;
    }
}
