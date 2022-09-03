using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TreasureCounter;
    private int treasureAmount;

    private void Awake()
    {
        treasureAmount = 0;
        TreasureCounter.text = "Treasure: " + treasureAmount.ToString();
    }

    public void UpdateTreasure(int amount)
    {
        treasureAmount += amount;
        TreasureCounter.text = "Treasure: " + treasureAmount.ToString();
    }
}
