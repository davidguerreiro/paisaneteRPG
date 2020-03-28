
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioComponent : MonoBehaviour {
    
    public AudioClip[] audioClips;                              // Audioclips to use with this audio component instance.
    private AudioSource audio;                                  // Audio component reference.
    private int currentAudioClipIndex;                          // Current audio clip index assigned to the audio clip.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Play sound.
    /// </summary>
    /// <param name="soundClip">int - sound clip to play from the array of sounds.</param>
    public void PlaySound( int audioClip ) {

        if ( audio.isPlaying ) {
            audio.Stop();
        }

        audio.clip = audioClips[ audioClip ];
        this.currentAudioClipIndex = audioClip;
        
        audio.Play();
    }

    /// <summary>
    /// Play current sound.
    /// </summary>
    public void PlaySound() {

        if ( audio.isPlaying ) {
            audio.Stop();
        }

        audio.Play();
    }

    /// <summary>
    /// Stop audio sound.
    /// </summary>
    public void StopAudio() {
        audio.Stop();
    }

    /// <summary>
    /// Get current audio clip. Returns array
    /// index by default.
    /// </summary>
    /// <param name="returnsClipName">bool - optional - Returns clip name instead</param>
    /// <returns>string</returns>
    public string GetCurrenAudioClip( bool returnsClipName = false ) {
        if ( returnsClipName ) {
            return audio.clip.name;
        } else {
            return this.currentAudioClipIndex.ToString();
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get audio component reference.
        audio = GetComponent<AudioSource>();

        // set default audio clip value.
        this.currentAudioClipIndex = 0;
    }
}
