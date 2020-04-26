using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public GameObject slotPrefab;                                     // Slot prefab reference - used to instantiate and display new slot items in the inventory
    public const int numSlots = 5;                                    // Number of slots displayed in the inventory.
    Image[] itemImages = new Image[ numSlots ];                       // Array of item images.
    Item[] items = new Item[ numSlots ];                              // Array of items for the inventory.
    GameObject[] slots = new GameObject[ numSlots ];                  // Array of slots displayed in the inventory.

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
