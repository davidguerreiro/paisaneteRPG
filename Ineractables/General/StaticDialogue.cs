using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDialogue : MonoBehaviour {
    public string name;                                 // Name displayed at the top of the dialogue box.

    [TextArea(15,20)]
    public string[] paragraphs;                         // Dialogue paragraphs.
    private int counter;                                // Counter used to check which paragraph needs to be displayed.
    private bool playerColliding;                       // True if the player is colliding with the object that displays this dialoge.
    private AudioComponent audioComponent;              // Audio component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {
        ListenForPlayerInput();
    }

    /// <summary>
    /// Init static dialoge.
    /// </summary>
    /// <retunrs>IEnumerator</returns>
    public IEnumerator TriggerDialogue() {
        bool islast = false;

        // check if this is the first paragraph. If so, display the box.
        if ( this.counter == 0 ) {

            // freeze player movement.
            PlayerMovementController.instance.FreezePlayer();
            
            // set speaker name in the dialogue box.
            DialogueBox.instance.speakerName.SetName( this.name );

            // display elements
            DialogueBox.instance.Display();
            yield return new WaitForSeconds( .5f );

            DialogueBox.instance.speakerName.animationComponent.Display();
        }

        // reset paragraph
        DialogueBox.instance.content.Reset();
        
        // display paragraph.
        if ( this.counter < this.paragraphs.Length ) {
            
            // check if we are displaying the last paragraph.
            islast = ( this.counter == this.paragraphs.Length - 1 ) ? true : false;

            StartCoroutine( DialogueBox.instance.content.DisplayDialogueText( this.paragraphs[ this.counter ] , DialogueBox.instance.audioComponent, islast ) );
            this.counter++;
        } else {

            // close dialogue box.
            DialogueBox.instance.Hide();
            this.counter = 0;

            // allow player to move again.
            PlayerMovementController.instance.UnFreezePlayer();
        }
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D( Collision2D other ) {
        
        // check if this speaker object is colliding the player. If so, the player can trigger the dialogue box.
        if ( other.gameObject.tag == "Player" ) {
            this.playerColliding = true;
        }
    }

    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit2D( Collision2D other ) {
        
        // check if the player stops colliding this object, so dialogue cannot be triggered by the player.
        if ( other.gameObject.tag == "Player" ) {
            this.playerColliding = false;
        }
    }

    /// <summary>
    /// Listen for player input.
    /// </summary>
    private void ListenForPlayerInput() {

        // get player action only if the player is colliding the speaker.
        string objectFacedByPlayer = ( PlayerVisionRange.instance.GetClosestGameObject() != null ) ? PlayerVisionRange.instance.GetClosestGameObject().name : "";

        if ( this.playerColliding && objectFacedByPlayer == gameObject.name && Input.GetKeyDown( "z" ) ) {

            // display accept sound if is not either the last paragraph or the first one.
            if ( this.counter > 0 && this.counter < this.paragraphs.Length ) {
                DialogueBox.instance.audioComponent.PlaySound( 2 );
            }

            // start dialogue
            if ( ! DialogueBox.instance.content.IsDisplayingText() ) {
                StartCoroutine( TriggerDialogue() );
            } else {
                DialogueBox.instance.content.StopDisplayingText( this.paragraphs[ this.counter - 1 ] );
            }
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // set initial counter value.
        this.counter = 0;

        // set initial value for the player colliding flag.
        this.playerColliding = false;
    }
}
