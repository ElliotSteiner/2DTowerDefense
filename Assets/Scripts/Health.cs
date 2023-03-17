using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    private EnemyMovement healthSystem;

    public TMP_Text healthText;
    public static int maxHealth = 20;
    public static int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString();
    }

    void TakeDamage(int amount) {
        currentHealth -= amount;
            if (currentHealth <= 0) {
            currentHealth = 0;
            SceneManager.LoadScene(sceneBuildIndex: 3);
             }
    }
    
    // Note: In order for the health system to work, the enemy must have a rigidbody that is stimulated and dynamic

    private void OnTriggerEnter2D(Collider2D collision) {

        healthSystem = collision.gameObject.GetComponent<EnemyMovement>();
       // Debug.Log(healthSystem.enemyHealth);
        if (healthSystem.enemyHealth == 50)
        {
            TakeDamage(1);
            healthText.text = currentHealth.ToString();
        }
        if(healthSystem.enemyHealth == 80)
        {
            TakeDamage(2);
            healthText.text = currentHealth.ToString();
        }
        //if (collision.gameObject.tag == "MinorEnemy") {
        //    TakeDamage(1);
        //    healthText.text = currentHealth.ToString();
        //    Debug.Log("Your Health: " + currentHealth);
        //}
        //if (collision.gameObject.tag == "MediumEnemy") {
        //    TakeDamage(2);
        //    healthText.text = currentHealth.ToString();
        //    Debug.Log("Your Health: " + currentHealth);
        //}
    }

   
}
