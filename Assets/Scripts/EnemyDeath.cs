using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;



    public class EnemyDeath : MonoBehaviour
    {
    public static float money = 300;
    public static float gems = 0; //For testing purposes

        Random rnd = new Random();

    public void StartMoney(int gold)
    {
        money = 0;
        money += gold;
    }
    

    public void GiveMoney(int gold)
        {
        //should play enemy death sound effect. just put it here to see if it would work.
        FindObjectOfType<AudioManager>().Play("EnemyDeath");
            money += gold;
            Debug.Log(money + " " + gold);
            //if (gameObject.isMinor)
            //{
            //    money += 5;
            //    Debug.Log("Your Wealth: " + money);
            //}
            //if (gameObject.isMedium)
            //{
            //    money += 15;
            //    Debug.Log("Your Wealth: " + money);
            //}
            int randNum = rnd.Next(1, 11);
            Debug.Log("Random #: " + randNum);
            if (randNum < 3)
            {
                gems += 5;
                Debug.Log("Gems: " + gems);
            }
        }
    public void LoseMoney(int gold)
    {
        money -= gold;
       
    }
        void Update()
        {
            //if (Health.currentHealth <= 0)
            //{

            //    Destroy(gameObject);
            //}
        }
    }

