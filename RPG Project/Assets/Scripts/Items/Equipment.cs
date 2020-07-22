using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    // Instead of public int equipSlot; we write
    public EquipmentSlot equipSlot; //slot to store equipment in
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions;

    public int armorModifier;   // increase/decrease in armor
    public int damageModifier;  // increase/decrease in damage

    // when pressed in inventory
    public override void Use()
    {
        base.Use();

        // equip the item
        EquipmentManager.instance.Equip(this);

        // remove it from the inventory
        RemoveFromInventory();
    }
}

// we dont want to encapsulate our equimentslot tracking by out equipment class
// can lead or prone to errors, we want to use this in multiple places
// thus use enum

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }
public enum EquipmentMeshRegion { Legs, Arms, Torso }; // Corresponds to body blendshapes