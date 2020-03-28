using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComponent : MonoBehaviour {
    public string displayAnimationName = "Display";                             // Display animation name.
    public string hideAnimationName = "Hide";                                   // Hide animation name.
    public bool displayed;                                                     // Displayed status.
    private Animation animation;                                                // Animation component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Get displayed status.
    /// </summary>
    /// <returns>bool</returns>
    public bool IsDisplayed() {
        return this.displayed;
    }

    /// <summary>
    /// Update displayed flag.
    /// </summary>
    /// <param name="newValue">bool - new displayed value.false</param>
    public void UpdateDisplayed( bool newValue ) {
        this.displayed = newValue;
    }

    /// <summary>
    /// Display element.
    /// </summary>
    public void Display() {
        Utils.instance.TriggerAnimation( animation, this.displayAnimationName );
        this.displayed = true;
    }

    /// <summary>
    /// Hide element.
    /// </summary>
    public void Hide() {
        Utils.instance.TriggerAnimation( animation, this.hideAnimationName );
        this.displayed = false;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animation component.
        animation = GetComponent<Animation>();
    }
}
