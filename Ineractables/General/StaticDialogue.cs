using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDialogue : MonoBehaviour {
    public string name;                                 // Name displayed at the top of the dialogue box.

    [TextArea(15,20)]
    public string[] paragraphs;                         // Dialogue paragraphs.
    private int counter;                                // Counter used to check which paragraph needs to be displayed.
    private bool playerColliding;                       // True if the player is colliding with the object that displays this dialoge.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// <summary>
    /// Init static dialoge.
    /// </summary>
    /// <retunrs>IEnumerator</returns>
    public IEnumerator TriggerDialoge() {

        // check if this is the first paragraph. If so, display the box.
        if ( this.counter == 0 ) {
            DialogueBox.instance.Display();
            yield break;
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
