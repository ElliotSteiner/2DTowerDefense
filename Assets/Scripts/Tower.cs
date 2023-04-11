using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Tower : MonoBehaviour
{
    UpgradeOverlay overlay;
    Projectile projectile;

    [SerializeField] GameObject fireball;
    EnemyMovement enemyMovement;

    public Sprite Tier2;
    public Sprite Tier3;
    public Projectile projectileType;
    public TooltipTrigger tooltip;

    public EnemyDeath enemyDeathScript;

    public GameObject Upgrader;

    public int tier;

    [SerializeField]
    public float damageAmount;
    [SerializeField]
    private float range;
    [SerializeField]
    private float projectileMoveSpeed;
    [SerializeField]
    private float shootTimerMax;
    [SerializeField]
    public float reduceEnemySpeed;

    private float trueRange;
    private float trueSpeed;


    private float ogRange;
    private float ogSpeed;
   
    private float rangeIncrease;

    private float speedIncrease;

    private int lookedOut;

    private int pickTower;

    private int lookoutTier;


    public bool isBoulder;
    public bool isLookout;
    public bool isWizard;
    public bool isArcher;
    public bool isCannon;
    public bool isDruid;

    private Transform bullet;

    private Vector3 projectileShootFromPosition;
    
    //private float shootTimerMax;
    private float shootTimer;

    public static List<Tower> towerList = new List<Tower>();

    public static void GetTowersInRange(Vector3 position, float maxRange, int lookedOut, int tier)
    {
        // EnemyMovement closest = null;

        foreach (Tower tower in towerList)
        {
            


            if (Vector3.Distance(position, tower.GetPosition()) <= maxRange)
            {

                if (lookedOut == 0) {
                   
                    tower.lookoutUpgrade(lookedOut, tower, tier);
                }
                //Debug.Log("IN RANGE");
                //Debug.Log(tower);
            }
        }

    }


    private void lookoutUpgrade(int lookedOut, Tower tower, int tier)
    {

        lookoutTier = tier;
        
       // Debug.Log("TIER = " + lookoutTier);
        //Debug.Log("Range: " + tower.range + " " + "ShootSpeed: " + tower.shootTimerMax);
        //Debug.Log("lookedOutValue: "+tower.lookedOut);
        if(tower.lookedOut == 0)
        {
            Debug.Log("Range Increased: "+ (rangeIncrease));
            tower.trueRange = (tower.ogRange * rangeIncrease);
            tower.trueSpeed = (tower.ogSpeed * speedIncrease);
            tower.range = tower.range + trueRange;
            tower.shootTimerMax = tower.shootTimerMax - trueSpeed;
            
            
            tower.lookedOut = 1;
        }
        
        if (tier == 2 && tower.lookedOut == 1)
        {
           
            tower.trueRange = (tower.ogRange * rangeIncrease);
            tower.trueSpeed = (tower.ogSpeed * speedIncrease);
            tower.range = tower.range + trueRange;
            tower.shootTimerMax = tower.shootTimerMax - trueSpeed;

            tower.lookedOut = 2;
        }
        if (tier == 3 && tower.lookedOut == 2)
        {
            tower.trueRange = (tower.ogRange * rangeIncrease);
            tower.trueSpeed = (tower.ogSpeed * speedIncrease);
            tower.range = tower.range + trueRange;
            tower.shootTimerMax = tower.shootTimerMax - trueSpeed;

            tower.lookedOut = 3;
        }
        if(tower.shootTimerMax < 0.1f)
        {
            tower.shootTimerMax = 0.5f;
        }
    }

    private void Awake()
    {
        tooltip = Upgrader.GetComponent<TooltipTrigger>();
        Debug.Log(EnemyDeath.money);
        enemyDeathScript = new EnemyDeath();
        enemyMovement = gameObject.GetComponent<EnemyMovement>();
       
        overlay = gameObject.GetComponent<UpgradeOverlay>();
        if (isWizard)
        {
            projectile = fireball.GetComponent<Projectile>();
            projectile.gameObject.GetComponent<SpriteRenderer>().sprite = projectile.magicBolt;
        }

        ogRange = range;
        ogSpeed = shootTimerMax;
        trueRange = 0f;
        trueSpeed = 0f;
        rangeIncrease = 0.5f;
        speedIncrease = 0.5f;
        if (!isLookout)
        {
            towerList.Add(this);
            lookedOut = 0;
        }
        projectileShootFromPosition = transform.Find("ProjectileShootFromPosition").position;
        //shootTimerMax = .4f;
        //range = 5f;
        tier = 1;
    }

    private void Update()
    {
        


        if (isLookout)
        {
            GetTowersInRange(transform.position, range, lookedOut, tier);
        }
        if (Input.GetMouseButtonDown(0))
        {
            //Projectile.Create(projectileShootFromPosition, Testing.GetMouseWorldPosition());
        }
        //Debug.Log("ShootTimer: " + shootTimer);
       shootTimer -= Time.deltaTime;
        

        if (shootTimer <= 0f)
        {
            //shootTimer = shootTimerMax;

            EnemyMovement enemy = GetClosestEnemy();
            if (enemy != null)
            {
                if (!isLookout)
                {
                    whichTower();
                    // Debug.Log("Enemy in range");
                    Projectile.Create(projectileShootFromPosition, enemy, damageAmount, reduceEnemySpeed, bullet);
                    shootTimer = shootTimerMax;
                }
            }
           
        }

    }

    private void whichTower()
    {
        if (isArcher)
        {
            bullet = GameAssets.i.pfProjectileArcher;
        }
        else if (isWizard)
        {
            bullet = GameAssets.i.pfProjectileWizard;
        }
        else if (isBoulder)
        {
            bullet = GameAssets.i.pfProjectileBoulder;
        }
        else if (isCannon)
        {
            bullet = GameAssets.i.pfProjectileCannon;
        }
        else if (isDruid)
        {
            bullet = GameAssets.i.pfProjectileDruid;
        }
       
    }

    private EnemyMovement GetClosestEnemy()
    {
       return EnemyMovement.GetClosestEnemy(transform.position, range);
    }

   

    public float GetRange()
    {
        return range;
    }

    public float GetDamage()
    {
        return damageAmount;
    }

    //public void upgradeRange()
    //{
    //    range += (range / 6);
    //}

    //public void upgradeDamage()
    //{
    //    damageAmount += 5;
    //}
    public void upgradeTower()
    {
        
        if (tier == 2 && EnemyDeath.money >= 1000)
        {
            if (isWizard)
            {
                // Debug.Log("is Wizard!");
                projectile.changeSprite();
                projectile.gameObject.GetComponent<SpriteRenderer>().sprite = projectile.fireBall;
            }
            upgradeStats();
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Tier3;
            tier = 3;
            if (isLookout)
            {
                rangeIncrease = 1f;
                speedIncrease = 1f;
            }
            UpgradeOverlay.HideButton();
            enemyDeathScript.LoseMoney(1000);
        }
        if (tier == 1 && EnemyDeath.money >= 300)
        {
           
            upgradeStats();
            enemyDeathScript.LoseMoney(300);
            
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Tier2;
            tier = 2;
            
            if (isLookout)
            {
                rangeIncrease = 0.75f;
                speedIncrease = 0.75f;
            }
            
        }
        
        
    }

    private void upgradeStats()
    {
        range += (range / 6);
        damageAmount += (damageAmount * 0.5f);
        if (isBoulder)
        {
            projectileType.UpgradeBoulder();
        }
        if (reduceEnemySpeed < 1)
        {
            reduceEnemySpeed = (reduceEnemySpeed * 0.5f);
        }
    }

    private void OnMouseEnter()
    {
        Tower tower = gameObject.GetComponent<Tower>();

        
        if (tower.tier != 3)
        {
            UpgradeOverlay.ShowButton();
        }
        
        else if(tower.tier == 3)
        {
            UpgradeOverlay.HideButton();
        }
            UpgradeOverlay.Show_Static(this);
        
    }
    public void CloseOverlay()
    {
        UpgradeOverlay.Hide_Static();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
   
}

