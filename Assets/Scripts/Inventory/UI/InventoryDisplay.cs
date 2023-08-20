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
        bool isShiftPressed = Keyboard.current.leftShiftKey.isPressed; //REWORK TO NEW INPUT SYSTEM

        if (clickedUISlot.AssignedInventorySlot.ItemData != null && _mouseInventoryItem.AssidnedInventorySlot.ItemData == null)
        {
            if (isShiftPressed && clickedUISlot.AssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot))
            {
                _mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedUISlot.UpdateUISlot();
                return;
            }
            else
            {
                _mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
                clickedUISlot.ClearSlot();
                return;
            }
        }

        if (clickedUISlot.AssignedInventorySlot.ItemData == null && _mouseInventoryItem.AssidnedInventorySlot.ItemData != null)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(_mouseInventoryItem.AssidnedInventorySlot);
            clickedUISlot.UpdateUISlot();

            _mouseInventoryItem.ClearSlot();
            return;
        }

        if (clickedUISlot.AssignedInventorySlot.ItemData != null && _mouseInventoryItem.AssidnedInventorySlot.ItemData != null)
        {
            bool isSameItem = clickedUISlot.AssignedInventorySlot.ItemData == _mouseInventoryItem.AssidnedInventorySlot.ItemData;
            if (isSameItem && clickedUISlot.AssignedInventorySlot.EnoughRoomLeftInStack(_mouseInventoryItem.AssidnedInventorySlot.StackSize))
            {
                clickedUISlot.AssignedInventorySlot.AssignItem(_mouseInventoryItem.AssidnedInventorySlot);
                clickedUISlot.UpdateUISlot();
                _mouseInventoryItem.ClearSlot();
                return;
            }
            else if (isSameItem && !clickedUISlot.AssignedInventorySlot.EnoughRoomLeftInStack(_mouseInventoryItem.AssidnedInventorySlot.StackSize, out int leftInStack)) 
            {
                if (leftInStack < 1)
                    SwapSlots(clickedUISlot);
                else
                {
                    int remainingOnMouse = _mouseInventoryItem.AssidnedInventorySlot.StackSize - leftInStack;

                    clickedUISlot.AssignedInventorySlot.AddToStack(leftInStack);
                    clickedUISlot.UpdateUISlot();

                    var newItem = new InventorySlot(_mouseInventoryItem.AssidnedInventorySlot.ItemData, remainingOnMouse);
                    _mouseInventoryItem.ClearSlot();
                    _mouseInventoryItem.UpdateMouseSlot(newItem);
                    return;
                }
            }
            if (!isSameItem)
            {
                SwapSlots(clickedUISlot);
                return;
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
