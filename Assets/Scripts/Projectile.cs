using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 20f;


    public static void Create(Vector3 spawnPosition, Vector3 targetPosition)
    {
       Transform arrowTransform = Instantiate(GameAssets.i.pfProjectile, spawnPosition, Quaternion.identity);

        Projectile projectile = arrowTransform.GetComponent<Projectile>();
        projectile.Setup(targetPosition);
    }

    private Vector3 targetPosition;

    private void Setup(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
    
    private void Update()
    {
        Vector3 moveDir = (targetPosition - transform.position).normalized;

  

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        float destroySelfDistance = 1f;
        if (Vector3.Distance(transform.position, targetPosition) < destroySelfDistance)
        {
            Destroy(gameObject);
        }
    }

}
