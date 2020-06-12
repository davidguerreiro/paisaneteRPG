using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerAmmo")]
public class PlayerAmmo : ScriptableObject {
    public int value;                              // Current player ammo.
    public int maximun;                            // MaxValue.
}
