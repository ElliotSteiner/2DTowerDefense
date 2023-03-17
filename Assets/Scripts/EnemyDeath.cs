using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyDeath : MonoBehaviour
{
    public static float money = 200;
    public static float gems = 0;
    public EnemyHealth enemyHealth;

    Random rnd = new Random();


    void Update()
    {
        if (enemyHealth.GetHealth() <= 0)
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
                gems += 100;
                Debug.Log("Gems: " + gems);
            }
            Destroy(gameObject);
        }
    }
}
