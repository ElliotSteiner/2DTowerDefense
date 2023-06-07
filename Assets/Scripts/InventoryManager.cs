using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Utils;

public class InventoryManager : MonoBehaviour
{
    public GameObject eventSystem;

    public GameObject itemOne;
    public TMP_Text textOne;
    public GameObject itemTwo;
    public TMP_Text textTwo;
    public GameObject itemThree;
    public TMP_Text textThree;

    public HeartIconPulse heartIconPulse;

    public GameObject fireEffect;
    public FireOrb fireOrbScript;

    public GameObject blizzard;


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
        if (itemID == 1)
        {
            shopItems[3, 1]--;
            textOne.text = shopItems[3, 1].ToString();
            if (shopItems[3, itemID] <= 0)
            {
                shopItems[3, itemID] = 0;
                textOne.text = shopItems[3, 1].ToString();
            }
        }
        if (itemID == 2)
        {
            shopItems[3, 2]--;
            textTwo.text = shopItems[3, 2].ToString();
            if (shopItems[3, itemID] <= 0)
            {
                shopItems[3, itemID] = 0;
                textTwo.text = shopItems[3, 2].ToString();
            }
        }
        if (itemID == 3)
        {
            shopItems[3, 3]--;
            textThree.text = shopItems[3, 3].ToString();
            if (shopItems[3, itemID] <= 0)
            {
                shopItems[3, itemID] = 0;
                textThree.text = shopItems[3, 3].ToString();
            }
        }
    }


    public void Purchase()
    {
        GameObject ButtonRef = EventSystem.current.currentSelectedGameObject;

        if (EnemyDeath.gems >= shopItems[2, ButtonRef.GetComponent<ShopButtonInfo>().itemID])
        {
            EnemyDeath.gems -= shopItems[2, ButtonRef.GetComponent<ShopButtonInfo>().itemID];
            shopItems[3, ButtonRef.GetComponent<ShopButtonInfo>().itemID]++;
            ButtonRef.GetComponent<ShopButtonInfo>().quantityText.text = shopItems[3, ButtonRef.GetComponent<ShopButtonInfo>().itemID].ToString();
        }
    }


    public void ItemEffect(int itemID, Vector3 position)
    {
        if (itemID == 1)//Deal damage to enemies within a certain radius of usage
        {
            Instantiate(fireEffect, position, transform.rotation);
        }
        if (itemID == 2)//Heal player 3 health
        {
            int amountToHeal = 3;

            for (int i = 0; i < amountToHeal; i++)
            {
                heartIconPulse.Pulse();
            }
        }
        if (itemID == 3)//Freeze all enemies in place for a certain amount of time
        {
            Instantiate(blizzard, new Vector3(0, 0, 0), transform.rotation);
            StartCoroutine(Freeze());
        }
    }

    IEnumerator Freeze()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //Start slowing
        for (int i = 10; i > 0; i--)
        {
            yield return new WaitForSeconds(0.5f);
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyMovement>().Damage(1, (float)(0.1 * i));
            }
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }

        //Freeze them
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyMovement>().Damage(5, 0);
        }
        yield return new WaitForSeconds(4f);

        //Speed them back to normal
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.5f);
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyMovement>().Damage(0, (float)(0.1 * i));
            }
        }
    }

}
