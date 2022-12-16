using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public float enemyHealth;
    public static float money = 200;

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
            Destroy(gameObject);
        }
    }
}
