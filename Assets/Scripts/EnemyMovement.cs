using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    private Waypoints wpoints;

    private int waypointIndex;

    public static List<EnemyMovement> enemyList = new List<EnemyMovement>();

    public static EnemyMovement GetClosestEnemy(Vector3 position, float maxRange)
    {
        EnemyMovement closest = null;
        foreach (EnemyMovement enemy in enemyList)
        {
            //if (enemy.IsAlive()) continue;
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
