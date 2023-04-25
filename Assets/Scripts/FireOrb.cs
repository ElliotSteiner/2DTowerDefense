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
        if (collision.gameObject.tag == "Enemy")
        {
            enemyMovement.Damage(5, 1);
        }
    }
}
