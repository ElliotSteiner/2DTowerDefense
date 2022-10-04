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
        Debug.Log("Kira");
    }

    void TakeDamage(int amount) {
        Debug.Log("potato");
        currentHealth -= amount;
            if (currentHealth <= 0) {
            currentHealth = 0;
             }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Ominous");
        if (collision.tag == "MinorEnemy") {
            TakeDamage(1);
            Debug.Log(currentHealth);
        }
    }

   
}
