using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public static int maxHealth = 20;
    public static int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Your Health: " + currentHealth);
    }

    void TakeDamage(int amount) {
        currentHealth -= amount;
            if (currentHealth <= 0) {
            currentHealth = 0;
             }
    }

    // Note: In order for the health system to work, the enemy must be tagged correctly and have a rigidbody that is stimulated and dynamic

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "MinorEnemy") {
            TakeDamage(1);
            Debug.Log("Your Health: " + currentHealth);
        }
    }

   
}
