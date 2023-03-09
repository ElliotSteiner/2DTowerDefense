using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField]
    int maxHealth;

    private HealthBar healthBar;

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
        enemyList.Add(this);
        //Debug.Log(healthSystem.GetHealth());
        healthSystem = new EnemyHealth(100);
        healthSystem.SetHealthMax(maxHealth, true);
        //SetEnemyType();
       

    }

    

    

   

    private void SetEnemyType()
    {

    }

    public int GetHealth()
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

    public void Damage(int damageAmount)
    {
        healthSystem.Damage(damageAmount);
        Debug.Log(healthSystem.GetHealthPercent());
        healthBar.SetSize(healthSystem.GetHealthPercent());
        if (IsDead())
        {
            
            Destroy(gameObject);
            //foreach (EnemyMovement enemy in enemyList)
            //{
               // Debug.Log("NEW LIST: " + enemy.ToString());
            //}
        }
        foreach (EnemyMovement enemy in enemyList)
        {
          
            Debug.Log("Damaged enemy: " + GetHealth());
        }
        
    }



    

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, wpoints.waypoints[waypointIndex].position, speed * Time.deltaTime);

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
