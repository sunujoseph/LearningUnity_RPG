using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    // creating a static variable, shared by all instances of the class Inventory
    // when starting the game, setting this instance to this component 
    // always able to access this certain inventory component by going inventory.instance

    // mean that you should have 1 inventory at all times
    // singletons with region, helps organize code


    #region Singleton
    public static Inventory instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    #endregion


    // delegates event subsribe different methods to
    // when event triggers, all of the subsribed methods will be called

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback; // whenever something chaning in our inventory we call

    public int space = 20;
    
    public List<Item> items = new List<Item>();

    public bool Add (Item item)
    {
        //check if items is not a default item

        if (!item.isDefaultItem)
        {
            // check if inventory space is exceed
            if (items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }

            items.Add(item);

            // after adding new item
            // triggering the event for updating the ui

            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }

            
        }

        return true;
        
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
