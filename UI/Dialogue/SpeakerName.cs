using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeakerName : MonoBehaviour {
    public AnimationComponent animationComponent;                      // Animation component reference.
    private Color originalColor;                                         // Original text color.
    private TextComponent textComponent;                                // Text component reference.
    
    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Reset component.
    /// </summary>
    public void ResetComponent() {
        
        animationComponent.UpdateDisplayed( false );

        // set original color.
        textComponent.UpdateColour( originalColor );

        textComponent.UpdateContent( "" );
    }

    /// <summary>
    /// Set speaker name.
    /// <param name="speakerName">string - speaker name</param>
    /// </summary>
    public void SetName( string speakerName ) {
        textComponent.UpdateContent( speakerName );
    }


    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get text component reference.
        textComponent = GetComponent<TextComponent>();

        // get animation component.
        animationComponent = GetComponent<AnimationComponent>();

        // get original color.
        this.originalColor = GetComponent<Text>().color;
    }

}
