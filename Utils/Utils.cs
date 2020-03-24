using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour {
    public static Utils instance;                       // Public class instance reference to make this class available in other scripts.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        if ( instance == null ) {
            instance = this;
        }
    }

    /// <summary>
    /// Trigger an animation.
    /// </summary>
    /// <param name="animation">Animation - animation component reference.</param>
    /// <param name="clipName">string - clip name to be played</param>
    /// <param name="loop">bool - wheter to keep playing the animtion in a loop</param>
    public void TriggerAnimation( Animation animation, string clipName, bool loop = false ) {
        if ( animation.isPlaying ) {
            animation.Stop();
        }

        // set animation to play in a loop if required.
        if ( loop ) {
            animation.wrapMode = WrapMode.Loop;
        } else {
            animation.wrapMode = WrapMode.Once;
        }

        // get clip from animation array of clips.
        animation.clip = animation.GetClip( clipName );

        animation.Play();
    }
}
