using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour {
    public static DialogueBox instance;                                 // Public static class instance.
    public SpeakerName speakerName;                                     // Dialogue speaker name displayed at the top of the dialogue box.
    public Content content;                                             // Content displayed on the dialogue box.
    public AudioComponent audioComponent;                              // Audio component refernce.
    private AnimationComponent animationComponent;                      // Animation Component reference.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        if ( instance == null ) {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// <summary>
    /// Display dialoge box.
    /// </summary>
    public void Display() {
        audioComponent.PlaySound( 0 );
        animationComponent.Display();
    }

    /// <summary>
    /// Hide dialoge box.
    /// </summary>
    public void Hide() {
        // TODO: Add remove box audio call here.

        // reset dialoge box components.
        speakerName.ResetComponent();
        content.Reset();

        animationComponent.Hide();
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animation component reference.
        animationComponent = GetComponent<AnimationComponent>();
    }
}
