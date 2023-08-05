using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData _mouseInventoryItem;

    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;
    
    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

    protected virtual void Start()
    {

    }
    public abstract void AssignSlot(InventorySystem invToDisplay);
    public virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach (var slot in SlotDictionary)
        {
            if (slot.Value == updatedSlot)
            {
                slot.Key.UpdateUISlot(updatedSlot);
            }
        }
    }    

    public void SlotClicked(InventorySlot_UI clickedUISlot)
    {
        if (clickedUISlot.AssignedInventorySlot.ItemData != null && _mouseInventoryItem.AssidnedInventorySlot.ItemData == null)
        {
            _mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
            clickedUISlot.ClearSlot();
            return;
        }

        if (clickedUISlot.AssignedInventorySlot.ItemData == null && _mouseInventoryItem.AssidnedInventorySlot.ItemData != null)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(_mouseInventoryItem.AssidnedInventorySlot);
            clickedUISlot.UpdateUISlot();

            _mouseInventoryItem.ClearSlot();
        }

        if (clickedUISlot.AssignedInventorySlot.ItemData != null && _mouseInventoryItem.AssidnedInventorySlot.ItemData != null)
        {
            if (clickedUISlot.AssignedInventorySlot.ItemData != _mouseInventoryItem.AssidnedInventorySlot.ItemData)
            {
                SwapSlots(clickedUISlot);
            }
        }
    }

    private void SwapSlots(InventorySlot_UI clickedUISlot)
    {
        var clonesSlot = new InventorySlot(_mouseInventoryItem.AssidnedInventorySlot.ItemData, _mouseInventoryItem.AssidnedInventorySlot.StackSize);
        _mouseInventoryItem.ClearSlot();

        _mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
        clickedUISlot.ClearSlot();

        clickedUISlot.AssignedInventorySlot.AssignItem(clonesSlot);
        clickedUISlot.UpdateUISlot();
    }
}
