using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class EnemyHealth
    {
        private int health;
        private int healthMax;


        public EnemyHealth(int healthMax)
        {
            this.healthMax = healthMax;
            health = healthMax;
        }


        public void Damage(int damage)
        {
            health -= damage;
            if (health < 0)
            {
                health = 0;
            }
        }

        public bool IsDead()
        {
            return health <= 0;
        }

        public void SetHealthMax(int healthMax, bool fullHealth)
        {
            this.healthMax = healthMax;
            if (fullHealth) health = healthMax;

        }
    }
}
