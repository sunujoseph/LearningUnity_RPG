using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

// Keep track of equipment. has functions for adding and removing items

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    Inventory inventory;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public Equipment[] defaultItems;

    public SkinnedMeshRenderer targetMesh;

    Equipment[] currentEquipment; // items we currently have equipped
    SkinnedMeshRenderer[] currentMeshes;


    // Callback for when an item is equipped/unequipped
    public delegate void OnEquipmentChange(Equipment newItem, Equipment oldItem);
    public OnEquipmentChange onEquipmentChange;

    void Start()
    {
        inventory = Inventory.instance; //reference to our inventory 

        // Initialize currentEquipment based on the number of quipment slots
       int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];

        EquipDefaultItems();
    }

    // equiping the item
    // each equipment slot in enum EquipmentSlot in EquipmentManager has a index

    public void Equip (Equipment newItem)
    {
        // find out what slot the item fits in
        int slotIndex = (int)newItem.equipSlot;

        //Unequip(slotIndex);

        Equipment oldItem = Unequip(slotIndex); 

        /*
         * if there was already an item in the slot
         * make sure to put back in the inventory
         * if (currentEquipment[slotIndex] != null)
         * {
         *   oldItem = currentEquipment[slotIndex];
         *   inventory.Add(oldItem);
         * }
         * 
         */


        if (onEquipmentChange != null)
        {
            onEquipmentChange.Invoke(newItem, oldItem);
        }

        SetEquipmentBlendShapes(newItem, 100);

        // insert item into the slot
        currentEquipment[slotIndex] = newItem;

        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);

        //target mesh is refering to player mesh
        newMesh.transform.parent = targetMesh.transform;

        // we want the newMesh to deform based on on the bones on the targetMesh
        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;

        currentMeshes[slotIndex] = newMesh;
    }

    // unequip an item with a particular index
    public Equipment Unequip(int slotIndex)
    {
        //only do this if an item is there
        if (currentEquipment[slotIndex] != null)
        {
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }

            // add the item to theinventory
            Equipment oldItem = currentEquipment[slotIndex];

            SetEquipmentBlendShapes(oldItem, 0);

            inventory.Add(oldItem);

            //remove the item from the equipment array
            currentEquipment[slotIndex] = null;

            // equipment has been removed so we trigger the callback
            if (onEquipmentChange != null)
            {
                onEquipmentChange.Invoke(null, oldItem);
            }

            return oldItem;
        }
        return null;

    }

    // Unequip all items
    public void UnequipAll ()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
        EquipDefaultItems();
    }

    // reference to the blend shapes slider
    void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    void EquipDefaultItems()
    {
        foreach (Equipment item in defaultItems)
        {
            Equip(item);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            // unequip all items if we press U
            UnequipAll();
        }
    }



}
