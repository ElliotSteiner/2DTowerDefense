using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    public float coins;
    public GameObject eventSystem;
    public GameObject itemOne;
    public GameObject itemTwo;
    public GameObject itemThree;

    public Health healthScript;
    public EnemyMovement enemyMovement;

    public GameObject fireEffect;
    public FireOrb fireOrbScript;

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

    IEnumerator Freeze()
    {
        for (float x = 0; x < 6; x += 0.2f)
        {
            //enemyMovement.Damage(0f, 0.03f);
            yield return new WaitForSeconds(0.05f);
        }
        

        //**--__|Replace these Damage's with Elliots technique for fireOrb. These don't work|__--**\\


        yield return new WaitForSeconds(2f);
        //enemyMovement.Damage(0f, 0.1f);
        yield return new WaitForSeconds(4f);

        float growthFactor = 0.5f;

        for (float x = 0f; x < 10f; x += 0.4f)
        {
            //   enemyMovement.Damage(0f, -0.04);
            yield return new WaitForSeconds(growthFactor);
            growthFactor /= 1.2f;
        }
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
