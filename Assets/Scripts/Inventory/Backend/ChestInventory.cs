using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ChestInventory : InventoryHolder
{
   public void Interact()
    {
        OnDynamicInventoryDisplayRequested?.Invoke(inventorySystem);
    }
}
