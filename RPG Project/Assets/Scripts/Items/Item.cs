using UnityEngine;

// Make a blueprint for all our items
// like a class file


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    // 'new' overrides the current Item.name that is prebuilt. 
    // we can also do
    // public string itemName = "New Item"; 

    new public string name = "New Item"; // name of item
    public Sprite icon = null;          // item icon
    public bool isDefaultItem = false;  // is this item default wear?


    // for different items we want different thing when we try to use the item
    // some items sit in your inventory used as currency/crafting
    // some like usables like potions
    // some you can wear and equip like armor

    // Marking as virtual, we can derive different objects from the Item 
    // then define each one when its used

    // we can make sure to call this method for all them inside the InventorySlot 

    public virtual void Use()
    {
        // Use the item
        // something might happen

        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }

}
