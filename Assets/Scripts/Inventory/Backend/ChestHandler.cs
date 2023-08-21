using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHandler : InteractionHandler
{
    public ChestInventory _chestInventory;
    public override void Interaction(GameObject playerGameObject, GameObject otherGameObject)
    {
        _chestInventory.Interact();
    }
}
