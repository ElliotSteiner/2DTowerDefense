using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
public class Projectile : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 10f;


    public static void Create(Vector3 spawnPosition, EnemyMovement enemy, int damageAmount)
    {
       Transform arrowTransform = Instantiate(GameAssets.i.pfProjectile, spawnPosition, Quaternion.identity);

        Projectile projectile = arrowTransform.GetComponent<Projectile>();
        projectile.Setup(enemy, damageAmount);
    }

    private EnemyMovement enemy;
    private int damageAmount;



    private Vector3 targetPosition;

    private void Setup(EnemyMovement enemy, int damageAmount)
    {
        this.enemy = enemy;
        this.damageAmount = damageAmount;
    }
    
    private void Update()
    {
        if (enemy == null || enemy.IsDead()){
            Destroy(gameObject);
            return;
        }

        Vector3 targetPosition = enemy.GetPosition();
        Vector3 moveDir = (targetPosition - transform.position).normalized;

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        

        float destroySelfDistance = 1f;
        if (Vector3.Distance(transform.position, targetPosition) < destroySelfDistance)
        {
         //   enemy.Damage(damageAmount);
            //Destroy(gameObject);
        }

        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject enemyCollide = collision.gameObject;
        if (enemyCollide.CompareTag("Enemy"))
        {
            enemyCollide.GetComponent<EnemyMovement>().Damage(damageAmount);
            Destroy(gameObject);
        }
        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    GameObject enemyCollide = collision.gameObject;
        //   enemyCollide.GetComponent<EnemyMovement>
        //    enemyCollide.Damage(damageAmount);
        //    Destroy(gameObject);

        //}
        
    }
}
