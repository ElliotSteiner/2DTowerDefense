using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemyMovement : MonoBehaviour
{



   

    [SerializeField]
    int maxHealth;

    public enum EnemyType
    {
        Minor,
        Medium,
    }



    public float speed;
    private Waypoints wpoints;

    private EnemyHealth healthSystem;

    private int waypointIndex;

    public static List<EnemyMovement> enemyList = new List<EnemyMovement>();

    public static EnemyMovement GetClosestEnemy(Vector3 position, float maxRange)
    {
        EnemyMovement closest = null;
       
        foreach (EnemyMovement enemy in enemyList)
        {
            
            if (enemy.IsDead())
            {
                Debug.Log(enemy + " " + enemy.IsDead());

                continue;
            }
            
            if (Vector3.Distance(position, enemy.GetPosition()) <= maxRange)
            {
                Debug.Log("IN RANGE");
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

    

    void Awake()
    {
        enemyList.Add(this);
        Debug.Log("Added to List");
        healthSystem = new EnemyHealth(100);
        SetEnemyType();
        

    }

    private void SetEnemyType()
    {

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
        Debug.Log(healthSystem.GetHealth());
        healthSystem.Damage(damageAmount);
        if (IsDead())
        {
            foreach(EnemyMovement enemy in enemyList)
            {
               // Debug.Log("LIST: "+ enemy.ToString());
            }
            Destroy(gameObject);
            foreach (EnemyMovement enemy in enemyList)
            {
               // Debug.Log("NEW LIST: " + enemy.ToString());
            }
        }
    }

   

    void Start()
    {
        wpoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();
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
