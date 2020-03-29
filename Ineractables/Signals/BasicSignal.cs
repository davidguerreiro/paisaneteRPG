using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSignal : MonoBehaviour {
    private AudioComponent audio;                                   // Audio component class reference.
    private bool colliding;                                         // Flag to control the player is in collision to the signal.
    private bool playingAudio;                                      // Flag to control collison audio is not played several times at the same time.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {
        ListenForPlayerAction();
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
    /// Listen for player action.
    /// </summary>
    private void ListenForPlayerAction() {

        if ( this.colliding ) {

            // display sound when the player attemps to move towards the basic signal.
            string objectFacedByPlayer = ( PlayerVisionRange.instance.GetClosestGameObject() != null ) ? PlayerVisionRange.instance.GetClosestGameObject().name : "";

            if ( PlayerMovementController.instance.isMoving && objectFacedByPlayer == gameObject.name && ! this.playingAudio ) {
                StartCoroutine( PlayCollisionAudio() );
            }
        }
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D( Collision2D other ) {

        // trigger logic only with player stops colliding this object.
        if ( other.gameObject.tag == "Player" ) {
            Invoke( "SetPlayerCollided", .5f );
        }
        
    }

    /// <summary>
    /// Set player as colliding with this
    /// object.
    /// </summary>
    public void SetPlayerCollided() {
        this.colliding = true;
    }

    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit2D( Collision2D other ) {
        
        // trigger logic only when the player stops colliding this object.
        if ( other.gameObject.tag == "Player" ) {
            this.colliding = false;
        }
    }
    
    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get audio component reference.
        audio = GetComponent<AudioComponent>();

        // set default value for control flags.
        this.colliding = false;
        this.playingAudio = false;
    }
}
