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
        Init();
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// <summary>
    /// Instantiate inventory slots
    /// at runtime.
    /// </summary>
    private void CreateSlots() {

        if ( slotPrefab != null ) {

            for ( int i = 0; i < numSlots; i++ ) {
                
                // instantiate new slot
                GameObject newSlot = Instantiate( slotPrefab );
                newSlot.name = "ItemSlot_" + i;

                // set new slot in the hireachy as a part of the inventory item -  this is because this script is attached to the InventoryBackground
                newSlot.transform.SetParent( gameObject.transform );

                // keep a reference of the slot
                slots[i] = newSlot;

                // keep a reference of the image component for the slot
                itemImages[i] = newSlot.transform.GetChild(1).GetComponent<Image>();
                
            }
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {
        // display slots in the screen.
        CreateSlots();
    }

    /// <summary>
    /// Add item to the inventory.
    /// </summary>
    /// <param name="itemToAdd">Item - item to add in the slot</param>
    /// <returns>bool</returns>
    public bool AddItem( Item itemToAdd ) {

        for ( int i = 0; i < items.Length; i++ ) {

            // check if the item is already added in the inventory - if so, update item data.
            if ( items[i] != null && items[i].itemType == itemToAdd.itemType && itemToAdd.stackable == true ) {

                // adding item to existing slot.
                items[i].quantity = items[i].quantity + 1;


                // get slot components reference.
                Slot slotScript = slots[i].gameObject.GetComponent<Slot>();
                Text quantityText = slotScript.qtyText;

                // enable quantity text and update value.
                quantityText.enabled = true;
                quantityText.text = items[i].quantity.ToString();

                return true;
            }

            // item does not exist in the inventory - add item if there is space available in the slots.
            if ( items[i] == null ) {

                Debug.Log( i );

                // add item to empty slot
                items[i] = Instantiate( itemToAdd );
                
                // update item properties.
                items[i].quantity = 1;
                itemImages[i].sprite = itemToAdd.sprite;

                // enable item in the slot.
                itemImages[i].enabled = true;
                return true;
            }  
        }

        return false;
    }
}
