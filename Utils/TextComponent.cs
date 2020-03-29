﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextComponent : MonoBehaviour {

    private Text content;                                          // Text component reference.

    // Start is called before the first frame update
    void Start() { 
        Init();
    }

    /// <summary>
    /// Get current text
    /// content.
    /// </summary>
    /// <returns>string</returns>
    public string GetContent() {
        return content.text;
    }

    /// <summary>
    /// Update content.
    /// </summary>
    /// <param name="newContent">string - new content to be displayed in this text component.</param>
    public void UpdateContent( string newContent ) {
        content.text = newContent;
    }

    /// <summary>
    /// Update text colour.
    /// </summary>
    /// <param name="colour">color - Colour to apply to the text</param>
    public void UpdateColour( Color colour ) {
        content.color = colour;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    public void Init() {

        // get text component reference.
        content = GetComponent<Text>();
    }
}
