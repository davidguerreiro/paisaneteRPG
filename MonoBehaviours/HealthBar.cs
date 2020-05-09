using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public HitPoints hitPoints;                             // Hit points scriptable object refernece.
    public Player character;                                // Player character class reference.
    public Image meterImage;                                // Health bar image class reference.
    public Text hpText;                                     // hpText text class reference.
    private float maxHitPoints;                        // Max hit points.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        UpdateHealthBarComponent();
    }

    /// <summary>
    /// Update health bar values.
    /// This method must be called in
    /// Update()
    /// </summary>
    private void UpdateHealthBarComponent() {

        if ( character !=  null ) {
            meterImage.fillAmount = hitPoints.value / maxHitPoints;
            hpText.text = "HP:" + ( meterImage.fillAmount * 100 );
        }
    }

    /// <summary>
    /// Set up player variable.
    /// </summary>
    /// <param name="player">Player class instance reference.</param>
    public void SetPlayerReference( Player player ) {
        this.character = player;
        this.maxHitPoints = character.maxHitPoints;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get max hit points from the player.
        // this.maxHitPoints = character.maxHitPoints;
    }
}
