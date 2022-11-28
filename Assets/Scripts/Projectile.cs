using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
public class Projectile : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 10f;


    public static void Create(Vector3 spawnPosition, EnemyMovement enemy)
    {
       Transform arrowTransform = Instantiate(GameAssets.i.pfProjectile, spawnPosition, Quaternion.identity);

        Projectile projectile = arrowTransform.GetComponent<Projectile>();
        projectile.Setup(enemy);
    }

    private EnemyMovement enemy;



    private Vector3 targetPosition;

    private void Setup(EnemyMovement enemy )
    {
        this.enemy = enemy;
    }
    
    private void Update()
    {
        if (enemy == null){
            Destroy(gameObject);
            return;
        }

        Vector3 targetPosition = enemy.GetPosition();
        Vector3 moveDir = (targetPosition - transform.position).normalized;

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        

        float destroySelfDistance = 1f;
        if (Vector3.Distance(transform.position, targetPosition) < destroySelfDistance)
        {
            Destroy(gameObject);
        }
    }

}
