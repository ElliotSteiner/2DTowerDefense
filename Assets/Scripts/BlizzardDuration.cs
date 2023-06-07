using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlizzardDuration : MonoBehaviour
{
    public RectTransform rt;
    private void Start()
    {
        gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        rt.offsetMax = new Vector2(0, rt.offsetMax.y);
        rt.offsetMax = new Vector2(rt.offsetMax.x, 0);
        StartCoroutine(Stall());
    }

    IEnumerator Stall()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
