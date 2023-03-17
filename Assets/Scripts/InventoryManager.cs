using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    public float coins;
    public DraggableItem draggableItem;
    public GameObject eventSystem;
    public GameObject itemOne;
    public GameObject itemTwo;
    public GameObject itemThree;

    //The size of this array is just for extra storage. Many of those slots are empty
    public int[,] shopItems = new int[6, 6];


    public void Start()
    {
        //Shop item ID's
        shopItems[1, 1] = 1; //fire spirit
        shopItems[1, 2] = 2; //heatlh pot
        shopItems[1, 3] = 3; //frostbane

        //Shop item prices
        shopItems[2, 1] = 10;
        shopItems[2, 2] = 20;
        shopItems[2, 3] = 30;

        //Shop item quantity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
    }

    public void ItemUse(int itemID)
    {
        shopItems[3, draggableItem.GetComponent<DraggableItem>().itemID]--;
        Debug.Log("It works! Item Quantity: " + shopItems[3, draggableItem.GetComponent<DraggableItem>().itemID]);
    }

    public void Purchase()
    {
        coins = EnemyDeath.money;

        GameObject ButtonRef = EventSystem.current.currentSelectedGameObject;

        if (EnemyDeath.gems >= shopItems[2, ButtonRef.GetComponent<ShopButtonInfo>().itemID])
        {
            EnemyDeath.gems -= shopItems[2, ButtonRef.GetComponent<ShopButtonInfo>().itemID];
            shopItems[3, ButtonRef.GetComponent<ShopButtonInfo>().itemID]++;
            ButtonRef.GetComponent<ShopButtonInfo>().quantityText.text = shopItems[3, ButtonRef.GetComponent<ShopButtonInfo>().itemID].ToString();
            NewItem(ButtonRef);
        }
    }

    public void NewItem(GameObject ButtonRef) {//These tags are on the inventory slots
        GameObject newItem;
        if (ButtonRef.GetComponent<ShopButtonInfo>().itemID == 1)
        {
            newItem = Instantiate(itemOne, new Vector3(0, 0, 0), Quaternion.identity);
            newItem.transform.SetParent(GameObject.FindGameObjectWithTag("FireSpirit").transform,false);
        }
        if (ButtonRef.GetComponent<ShopButtonInfo>().itemID == 2)
        {
            newItem = Instantiate(itemTwo, new Vector3(0, 0, 0), Quaternion.identity);
            newItem.transform.SetParent(GameObject.FindGameObjectWithTag("Hpot").transform, false);
        }
        if (ButtonRef.GetComponent<ShopButtonInfo>().itemID == 3)
        {
            newItem = Instantiate(itemThree, new Vector3(0, 0, 0), Quaternion.identity);
            newItem.transform.SetParent(GameObject.FindGameObjectWithTag("Frostbane").transform, false);
        }
    }
}
