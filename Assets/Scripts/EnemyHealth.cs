using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//namespace Utils
//{
    public class EnemyHealth : MonoBehaviour
    {

     
        private float health;
        private int healthMax;
        public EnemyMovement enemyMovement;
    //private HealthBar healthBar;


        public EnemyHealth(int healthMax)
        {
            this.healthMax = healthMax;
            health = healthMax;
        }

        public float GetHealth()
        {
        return health;
        }

        public int GetMaxHealth()
        {
            return healthMax;
        }


        public void Damage(float damage, int enemyHealth)
        {
        health = (float)enemyHealth;
        health -= damage;
            if (health < 0)
            {
                health = 0;
            }
    }

        
        public void Heal(float amount)
    {
        health += amount;
        if(health > healthMax)
        {
            health = healthMax;
        }
    }
        public void SetHealthMax(int healthMax, bool fullHealth)
        {
        this.healthMax = healthMax;
            if (fullHealth) health = healthMax;
        }

        public bool IsDead()
        {
            return health <=0;
        }

        public float GetHealthPercent()
        {
        Debug.Log("Health before health bar calculations: " + health);
        Debug.Log("Max health at that time: " + healthMax);
        //SetHealthMax(enemyMovement.maxHealth, false);
        Debug.Log("Max health after I try to manually fix it: " + healthMax);
        return (float)health / healthMax;
        }
    }
//}
