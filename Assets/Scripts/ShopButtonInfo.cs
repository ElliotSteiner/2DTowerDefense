using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopButtonInfo : MonoBehaviour
{
    public int itemID;
    public TMP_Text quantityText;
    public GameObject inventoryManager;

    void Update()
    {
        quantityText.text = inventoryManager.GetComponent<InventoryManager>().shopItems[3, itemID].ToString();
    }
}
