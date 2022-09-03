using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : CollectibleBase
{
    [SerializeField] private int TreasureValue = 1;

    protected override void Collect(Player player)
    {
        Inventory inventory = player.GetComponent<Inventory>();
        if (inventory != null)
        {
            inventory.UpdateTreasure(TreasureValue);
        }
    }
}
