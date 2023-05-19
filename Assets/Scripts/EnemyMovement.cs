using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField]
    public int maxHealth;
    [SerializeField]
    private int goldAmount;

    public int hurtFactor;

    private HealthBar healthBar;
    private Tower tower;
    private float enemySpeed;

    public bool isMinor;
    public bool isMedium;
    public bool isHealer;
    public bool isShielder;
    public bool isSpeeder;

    public EnemyDeath enemyDeathScript;

    public float loopDuration = 10.0f;
    private float time = 0.0f;

    private bool gaveMoney;

    public int enemyHealth;


    void Start()
    {
        healthBar = gameObject.GetComponentInChildren<HealthBar>();
        Debug.Log(healthBar);
        wpoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();
        enemyHealth = maxHealth;
        Debug.Log("Health when movement script starts: " + enemyHealth);
    }

    public static List<EnemyMovement> enemyList = new List<EnemyMovement>();

    public static List<EnemyMovement> closeEnemies = new List<EnemyMovement>();

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

    public static void GetNearEnemies(Vector3 position, float maxRange)
    {
        

        foreach (EnemyMovement enemy in enemyList)
        {
            if (enemy.IsDead() || enemy == null) continue;
            if(Vector3.Distance(position, enemy.GetPosition()) <= maxRange)
            {
                closeEnemies.Add(enemy);
            }
        }
    }

    public enum EnemyType
    {
        Minor,
        Medium,
    }



    public float speed;

    private Waypoints wpoints;

    public EnemyHealth healthSystem;

    private int waypointIndex;

    void Awake()
    {
        gaveMoney = false;
        enemyDeathScript = new EnemyDeath();
        enemySpeed = speed;
        enemyList.Add(this);
        //Debug.Log(healthSystem.GetHealth());
        healthSystem = new EnemyHealth(100);
        healthSystem.SetHealthMax(maxHealth, true);
        //SetEnemyType();
        tower = FindObjectOfType<Tower>();
    }

    private void GetGold(int goldValue)
    {
        enemyDeathScript.GiveMoney(goldValue);
    }

    

  

    public void DealBoulder(Vector3 position, float maxRange, float damage)
    {
        foreach(EnemyMovement enemy in enemyList)
        {
            if (enemy.IsDead() || enemy == null) continue;
            if (Vector3.Distance(position, enemy.GetPosition()) <= maxRange)
            {
                //enemy.Damage(tower.damageAmount, tower.reduceEnemySpeed);
                enemy.Damage(damage, 1);
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
        Debug.Log("Health when damage is called: " + enemyHealth);
        healthSystem.Damage(damageAmount, enemyHealth);
        enemyHealth = (int)healthSystem.GetHealth();
        Debug.Log("Health after damage is done and code comes back to movement script:" + enemyHealth);
        Debug.Log("If code tries to calculate for Healthbar: " + healthSystem.GetHealthPercent());
        healthBar = gameObject.GetComponentInChildren<HealthBar>();
        Debug.Log(healthBar);
  
        healthBar.SetSize(healthSystem.GetHealthPercent());
        Debug.Log(healthBar);
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
            if (gaveMoney == false)
            {
                GetGold(goldAmount);
                gaveMoney = true;
            }
            else;
           
            
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
        if (isHealer)
        {
            GetNearEnemies(transform.position, 1f);
            foreach(EnemyMovement enemy in closeEnemies)
            {
                healthSystem.Heal(enemy.GetHealth / 20);
            }
        }

        if (isShielder)
        {

        }

        if (isSpeeder)
        {

        }


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
