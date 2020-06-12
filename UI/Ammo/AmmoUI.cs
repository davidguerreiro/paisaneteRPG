using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUI : MonoBehaviour {
    public TextComponent remainAmmo;                            // Remaining ammo UI text component class;
    public PlayerAmmo playerAmmo;                               // Current player ammo scriptable object.
    // Start is called before the first frame update

    // Update is called once per frame
    void Update() {

        // update player ammo in screen.
        remainAmmo.UpdateContent( playerAmmo.value.ToString() );
    }
}
