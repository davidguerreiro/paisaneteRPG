using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Content : MonoBehaviour {
    public float speed;                                 // Text displaying speed.
    public GameObject textArrow;                        // Text arrow displayed after all the content is displayed in the box.
    private Text content;                               // Text component reference.
    private bool displayingText;                        // Flag to control when the script is displaying text.

    // private string paragraph;                        // Paragraph to display in the text dialoge box.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// <summary>
    /// Stop displaying
    /// text. This will display all the
    /// content in the dialogue box.
    /// <param name="paragraph">string - paragraph to display as a part of the content</param>
    /// </summary>
    public void StopDisplayingText( string paragraph ) {
        if ( this.displayingText ) {
            this.displayingText = false;
            content.text = paragraph;
        }
    }
    

    /// <summary>
    /// Update content.
    /// Used to display all the text when
    /// the player presses the action
    /// button.
    /// </summary>
    /// <param name="paragraph">string - paragraph to display in the dialogue box</param>
    public void DisplayContent( string paragraph ) {
        content.text = paragraph;
    }

    /// <summary>
    /// Display text into the 
    /// dialogue box.
    /// </summary>
    /// <param name="paragraph">string - content to display in the dialogue box</param>
    /// <param name="audio">AudioComponent - audio component to display sound effect in the dialogue box</param>
    /// <param name="isLast">bool - optional - wheter this paragraph is the last one or not. Used to check whether to display the text arrow in the dialogue box</param>
    /// <returns>IEnumerator</returns>
    public IEnumerator DisplayDialogueText( string paragraph, AudioComponent audio, bool isLast = false ) {
        this.displayingText = true;

        char[] characters = paragraph.ToCharArray();

        // internal counters - for loop or foreach loop cannot be used to ensure player can stop the text being displayed.
        int i = 0;

        while ( this.displayingText ) {
            
            // play letter sound.
            audio.PlaySound( 1 );

            content.text += characters[i];

            yield return new WaitForSeconds( speed );

            if ( i == characters.Length ) {
                this.displayingText = false;
            }
        }

        // display the text box if there are more paragraphs coming.
        if ( ! isLast ) {
            DisplayTextArrow();
        }

    }

    /// <summary>
    /// Display text arrow
    /// in the dialoge box.
    /// </summary>
    public void DisplayTextArrow() {
        textArrow.SetActive( true );
    }

    /// <summary>
    /// Hide text arrow.
    /// </summary>
    public void HideTextArrow() {
        textArrow.SetActive( false );
    }

    /// <summary>
    /// Reset component to
    /// initial status.
    /// </summary>
    public void Reset() {
        this.displayingText = false;
        content.text = "";
        HideTextArrow();
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get text component.
        content = GetComponent<Text>();

        // set default value for displaying text flag.
        this.displayingText = false;
    }
}
