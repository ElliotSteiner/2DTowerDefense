using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOrb : MonoBehaviour
{

    public EnemyMovement enemyMovement;
    public EnemyHealth enemy;
    float twentyPercentOfHP;

    void Start()
    {
        StartCoroutine(Stall());
    }

    IEnumerator Stall()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedEnemy = collision.gameObject;
    //    Debug.Log(collision.gameObject);

        if (collidedEnemy.tag == "Enemy")
        {
            twentyPercentOfHP = collidedEnemy.GetComponent<EnemyMovement>().maxHealth * 0.2f;
           

            StartCoroutine(Burn(collidedEnemy));
           
        }
    }


    IEnumerator Burn(GameObject collidedEnemy)
    {

        for (int i = 0; i < 25; i++)
        {
            collidedEnemy.GetComponent<EnemyMovement>().Damage(twentyPercentOfHP / 25, 1);
            yield return new WaitForSeconds(0.2f);
        }
    }

}
