using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "Item") ]

public class Item : ScriptableObject {
    public string objectName;                                   // Item name.
    public Sprite sprite;                                       // Object base sprite.
    public int quantity;                                        // Amount of this items which is collected.
    public bool stackable;                                      // Wheter this item will be auto-consumed or saved in the inventory.
    public enum ItemType {
        COIN,
        HEALTH,
        AMMO
    };

    public ItemType itemType;                                   // Type of item, or item class. Not visible for the player.
}
