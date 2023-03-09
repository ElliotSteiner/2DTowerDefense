using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField]
    int maxHealth;

    private HealthBar healthBar;
    private Tower tower;
    private float enemySpeed;

    public float boulderDamage;
    public float loopDuration = 10.0f;
    private float time = 0.0f;

    public int enemyHealth;


    void Start()
    {
        healthBar = gameObject.GetComponentInChildren<HealthBar>();
        wpoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();
        enemyHealth = maxHealth;
    }

    public static List<EnemyMovement> enemyList = new List<EnemyMovement>();

    public static EnemyMovement GetClosestEnemy(Vector3 position, float maxRange)
    {
        EnemyMovement closest = null;

        foreach (EnemyMovement enemy in enemyList)
        {

            if (enemy.IsDead() || enemy == null) continue;

            if (Vector3.Distance(position, enemy.GetPosition()) <= maxRange)
            {
                //Debug.Log("IN RANGE");
                if (closest == null)
                {
                    closest = enemy;
                }
                else
                {
                    if (Vector3.Distance(position, enemy.GetPosition()) < Vector3.Distance(position, closest.GetPosition()))
                    {
                        closest = enemy;
                    }
                }
            }
        }

        return closest;
    }


    public enum EnemyType
    {
        Minor,
        Medium,
    }



    public float speed;

    private Waypoints wpoints;

    private EnemyHealth healthSystem;

    private int waypointIndex;

    void Awake()
    {
        boulderDamage = 5f;
        enemySpeed = speed;
        enemyList.Add(this);
        //Debug.Log(healthSystem.GetHealth());
        healthSystem = new EnemyHealth(100);
        healthSystem.SetHealthMax(maxHealth, true);
        //SetEnemyType();
        tower = FindObjectOfType<Tower>();

    }

    

    

   public void BoulderSet(float boulderDamage)
    {
        
    }

    public void DealBoulder(Vector3 position, float maxRange)
    {
        foreach(EnemyMovement enemy in enemyList)
        {
            if (enemy.IsDead() || enemy == null) continue;
            if (Vector3.Distance(position, enemy.GetPosition()) <= maxRange)
            {
                //enemy.Damage(tower.damageAmount, tower.reduceEnemySpeed);
                enemy.Damage(boulderDamage, 1);
            }
        }
    }

    public float GetHealth()
    {
        return healthSystem.GetHealth();
    }

    public bool IsDead()
    {
        
        return healthSystem.IsDead();
    }

    //private void SetEnemyType(EnemyType enemyType)
    //{
    //    Material material;

    //    switch (enemyType)
    //    {
    //        default:
    //        case EnemyType.Minor:
    //            material = GameAssets.i.m_EnemyMinor;
    //            healthSystem.SetHealthMax(50, true);
    //            break;
    //        case EnemyType.Medium:
    //            material = GameAssets.i.m_EnemyMedium;
    //            healthSystem.SetHealthMax(80, true);
    //            break;
    //    }
    //}

    public void Damage(float damageAmount, float reducedSpeed)
    {
        healthSystem.Damage(damageAmount);
        Debug.Log(healthSystem.GetHealthPercent());
        healthBar.SetSize(healthSystem.GetHealthPercent());
        if (reducedSpeed < 1)
        {
            //enemySpeed = speed * reducedSpeed;
            //StartCoroutine(speedTimer());
            StartCoroutine(DoLoop(reducedSpeed));
            
        }
        //else
        //{
        //    enemySpeed = enemySpeed;
        //}
        

        if (IsDead())
        {
            
            Destroy(gameObject);
            //foreach (EnemyMovement enemy in enemyList)
            //{
               // Debug.Log("NEW LIST: " + enemy.ToString());
            //}
        }
        
        
    }

    IEnumerator DoLoop(float reducedSpeed)
    {
        do
        {
            enemySpeed = speed * reducedSpeed;
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        } while (time < loopDuration);
        time = 0.0f;
        enemySpeed = speed;
    }

    IEnumerator speedTimer()
    {
        yield return new WaitForSeconds(10.0f);
        enemySpeed = speed;


    }



    

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, wpoints.waypoints[waypointIndex].position, enemySpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, wpoints.waypoints[waypointIndex].position) < 0.1f) {
            if (waypointIndex < wpoints.waypoints.Length - 1)
            {
                waypointIndex++;
            }
            else {
                Destroy(gameObject);
                
            }
        }

        
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    
}
