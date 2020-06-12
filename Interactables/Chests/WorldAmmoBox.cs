using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldAmmoBox : MonoBehaviour {
    public int amount;                                                  // Ammo given by this box.
    public float secondsForRecharge;                                    // How long until the box has ammo recharged for the player to pick up.
    public PlayerAmmo playerAmmo;                                       // Player ammo scriptable object.
    private AudioComponent audio;                                       // Audio component script reference.
    private Coroutine recharging;                                       // Rechargin coroutine.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        ReadUserInput();
    }

    /// <summary>
    /// Read for user input.
    /// </summary>
    private void ReadUserInput() {

        // event only enabled if the player is looking at the box and the space key is pressed.
        GameObject playerLookingAt = PlayerVisionRange.instance.GetClosestGameObject();

        if ( playerLookingAt != null ) {
            if ( playerLookingAt.name == this.gameObject.name && Input.GetKeyDown( "space" ) ) {
                BoxConsumed();
            }
        }
    }

    /// <summary>
    /// Action triggered by the player
    /// when interacting with this box.
    /// Player's ammo is recharged.
    /// </summary>
    private void BoxConsumed() {

        if ( recharging == null ) {

            // display sound.
            audio.PlaySound( 0 );

            playerAmmo.value += amount;

            if ( playerAmmo.value > playerAmmo.maximun ) {
                playerAmmo.value = playerAmmo.maximun;
            }
            
            recharging = StartCoroutine( "RechargeBox" );

        } else {

            // display negative feedback sound.
            audio.PlaySound( 1 );
        }
    }

    /// <summary>
    /// Recharge time
    /// coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator RechargeBox() {
        yield return new WaitForSecondsRealtime( this.secondsForRecharge );
        recharging = null;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get audio component reference.
        audio = GetComponent<AudioComponent>();
    }

    
}
