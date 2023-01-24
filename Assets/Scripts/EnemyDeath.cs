using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyDeath : MonoBehaviour
{
    public float enemyHealth;
    public static float money = 200;
    public static float gems = 0;

    Random rnd = new Random();

    void Update()
    {
        if (enemyHealth <= 0)
        {
            if (gameObject.tag == "MinorEnemy")
            {
                money += 5;
                Debug.Log("Your Wealth: " + money);
            }
            if (gameObject.tag == "MediumEnemy")
            {
                money += 15;
                Debug.Log("Your Wealth: " + money);
            }
            int randNum = rnd.Next(1, 11);
            Debug.Log("Random #: " + randNum);
            if (randNum < 8)
            {
                gems += 10;
                Debug.Log("Gems: " + gems);
            }
            Destroy(gameObject);
        }
    }
}
