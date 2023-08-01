using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder _inventoryHolder;
    [SerializeField] private InventorySlot_UI[] _slots;
    protected override void Start()
    {
        base.Start();

        if (_inventoryHolder != null)
        {
            inventorySystem = _inventoryHolder.InventorySystem;
            inventorySystem.OnInventorySlotChanged += UpdateSlot;
        }
        else 
             Debug.Log($"No inventory assigned to {this.gameObject}");

        AssignSlot(inventorySystem);
    }

    public override void AssignSlot(InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if (_slots.Length != inventorySystem.InventirySize)
            Debug.Log($"Inventory slots out of sync on {this.gameObject}, part 3; 44.19");
        for (int i = 0; i < inventorySystem.InventirySize; i++)
        {
            slotDictionary.Add(_slots[i], inventorySystem.InventorySlots[i]);
            _slots[i].Init(inventorySystem.InventorySlots[i]);
        }
    }
}
