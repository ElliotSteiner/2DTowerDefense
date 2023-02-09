using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Tower : MonoBehaviour
{

    public Sprite Tier2;

    private int tier;

    [SerializeField]
    private int damageAmount;
    [SerializeField]
    private float range;

    private Vector3 projectileShootFromPosition;
    
    private float shootTimerMax;
    private float shootTimer;

    private void Awake()
    {
        projectileShootFromPosition = transform.Find("ProjectileShootFromPosition").position;
        shootTimerMax = .4f;
        //range = 5f;
        tier = 1;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Projectile.Create(projectileShootFromPosition, Testing.GetMouseWorldPosition());
        }

        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0f)
        {
            shootTimer = shootTimerMax;

            EnemyMovement enemy = GetClosestEnemy();
            if (enemy != null)
            {
               // Debug.Log("Enemy in range");
                Projectile.Create(projectileShootFromPosition, enemy, damageAmount);
            }
           
        }

    }

    private EnemyMovement GetClosestEnemy()
    {
       return EnemyMovement.GetClosestEnemy(transform.position, range);
    }

    public float GetRange()
    {
        return range;
    }

    public float GetDamage()
    {
        return damageAmount;
    }

    //public void upgradeRange()
    //{
    //    range += (range / 6);
    //}

    //public void upgradeDamage()
    //{
    //    damageAmount += 5;
    //}
    public void upgradeTower()
    {
        range += (range / 6);
        damageAmount += 10;
        if (tier == 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Tier2;
            tier = 2;
        }
    }

    private void OnMouseEnter()
    {
        UpgradeOverlay.Show_Static(this);
    }
    public void CloseOverlay()
    {
        UpgradeOverlay.Hide_Static();
    }
}
