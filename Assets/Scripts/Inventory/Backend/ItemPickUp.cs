using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : InteractionHandler
{

    public InventoryItemData ItemData;

    override public void Interaction(GameObject playerGameObject, GameObject otherGameObject)
    {
        if (playerGameObject.GetComponent<InventoryHolder>().InventorySystem.AddToInventory(ItemData, 1))
        {
            Destroy(this.gameObject);
        }
    }
}
