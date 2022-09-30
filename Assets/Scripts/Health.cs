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
    }

    void TakeDamage(int amount) {
        currentHealth -= amount;
            if (currentHealth <= 0) {
            currentHealth = 0;
             }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        TakeDamage(1);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
