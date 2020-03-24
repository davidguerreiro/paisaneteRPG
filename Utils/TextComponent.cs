using System.Collections;
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
    public void UpdateComponent( string newContent ) {
        content.text = newContent;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    public void Init() {

        // get text component reference.
        content = GetComponent<Text>();
    }
}
