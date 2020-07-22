using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {



        Debug.Log("Picking Up item." + item.name);

        // add to inventory

        // you can do: FindObjectOfType<Item>().Add()
        // but too taxing

        // if item was picked up but no room. then dont destroy the item

        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
        

    }

}
