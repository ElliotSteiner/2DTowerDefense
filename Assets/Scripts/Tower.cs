using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Tower : MonoBehaviour
{

    private Vector3 projectileShootFromPosition;
    private float range;
    private float shootTimerMax;
    private float shootTimer;

    private void Awake()
    {
        projectileShootFromPosition = transform.Find("ProjectileShootFromPosition").position;
        range = 10f;
        shootTimerMax = .5f;
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
                Projectile.Create(projectileShootFromPosition, enemy);
            }
        }
    }

    private EnemyMovement GetClosestEnemy()
    {
       return EnemyMovement.GetClosestEnemy(transform.position, range);
    }
}
