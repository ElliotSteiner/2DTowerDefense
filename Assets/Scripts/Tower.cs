using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Tower : MonoBehaviour
{

   
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

    public void upgradeRange()
    {
        range += (range / 6);
    }

    public void upgradeDamage()
    {
        damageAmount += 5;
    }

    private void OnMouseEnter()
    {
        UpgradeOverlay.Show_Static(this);
    }
    private void OnMouseExit()
    {
        UpgradeOverlay.Hide_Static();
    }
}
