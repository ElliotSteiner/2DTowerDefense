using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Utils;

public class EnemyMovement : MonoBehaviour
{

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
            if (enemy.IsDead()) continue;
            if (Vector3.Distance(position, enemy.GetPosition()) <= maxRange)
            {
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
        healthSystem = new EnemyHealth(100);

    }

    public bool IsDead()
    {
        return healthSystem.IsDead();
    }

    private void SetEnemyType(EnemyType enemyType)
    {
        Material material;

        switch (enemyType)
        {
            default:
            case EnemyType.Minor:
                material = GameAssets.i.m_EnemyMinor;
                healthSystem.SetHealthMax(80, true);
                break;
            case EnemyType.Medium:
                material = GameAssets.i.m_EnemyMedium;
                healthSystem.SetHealthMax(80, true);
                break;
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
