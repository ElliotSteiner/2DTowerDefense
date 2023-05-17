using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOrb : MonoBehaviour
{

    public EnemyMovement enemyMovement;

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
        Debug.Log(collision.gameObject);
        if (collidedEnemy.tag == "Enemy")
        {
            collidedEnemy.GetComponent<EnemyMovement>().Damage(5, 1);
            Debug.Log(collision.gameObject);
           //enemyMovement.Damage(5, 1);
        }
    }
}
