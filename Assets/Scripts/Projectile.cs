using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Utils
{
    public class Projectile : MonoBehaviour
    {

        EnemyMovement enemyMovement;
        public Sprite magicBolt;
        public Sprite fireBall;

        [SerializeField]
        float moveSpeed = 10f;
        [SerializeField]
        private float boulderRange;


        public bool isWizardProjectile;
        public Sprite upgradeWizard;



       
        

        public static void Create(Vector3 spawnPosition, EnemyMovement enemy, float damageAmount, float reducedSpeed, Transform towerProjectile)
        {

            Transform arrowTransform = Instantiate(towerProjectile, spawnPosition, Quaternion.identity);
            Projectile projectile = arrowTransform.GetComponent<Projectile>();
            projectile.Setup(enemy, damageAmount, reducedSpeed);

        }

        private EnemyMovement enemy;
        private float damageAmount;
        private float reducedSpeed;
        public bool isBoulderProjectile;


        public void changeSprite()
        {

            Debug.Log("ChangeSprite!!");
            //this.gameObject.GetComponent<SpriteRenderer>().sprite = fireBall;


        }

        private void Awake()
        {
            if (isWizardProjectile)
            {
                Debug.Log("Reset Sprite");
                //this.gameObject.GetComponent<SpriteRenderer>().sprite = magicBolt;
            }
        }

        private Vector3 targetPosition;

        private void Setup(EnemyMovement enemy, float damageAmount, float reducedSpeed)
        {
            this.enemy = enemy;
            this.damageAmount = damageAmount;
            this.reducedSpeed = reducedSpeed;
        }

        private void Update()
        {



            if (enemy == null || enemy.IsDead())
            {
                Destroy(gameObject);
                return;
            }

            Vector3 targetPosition = enemy.GetPosition();
            Vector3 moveDir = (targetPosition - transform.position).normalized;

            float angle = GetAngleFromVectorFloat(moveDir);
            transform.eulerAngles = new Vector3(0, 0, angle + 270);

            transform.position += moveDir * moveSpeed * Time.deltaTime;



            float destroySelfDistance = 1f;
            if (Vector3.Distance(transform.position, targetPosition) < destroySelfDistance)
            {
                //   enemy.Damage(damageAmount);
                //Destroy(gameObject);
            }


        }

        private void BoulderUpgrade()
        {

        }


        void OnTriggerEnter2D(Collider2D collision)
        {
            

            
        
        GameObject enemyCollide = collision.gameObject;
            if (enemyCollide.CompareTag("Enemy"))
            {
                enemyMovement = enemyCollide.GetComponent<EnemyMovement>();
                if (isBoulderProjectile)
                {
                   enemyMovement.DealBoulder(transform.position, boulderRange);
                }
                enemyMovement.Damage(damageAmount, reducedSpeed);
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

        public static float GetAngleFromVectorFloat(Vector3 dir)
        {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;

            return n;
        }
    }
}
