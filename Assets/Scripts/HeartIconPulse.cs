using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartIconPulse : MonoBehaviour
{
    public Health healthScript;

    public void Pulse()
    {
        StartCoroutine(Stall(1f));
        gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        healthScript.Heal();
        healthScript.UpdateHealthText();
        StartCoroutine(Stall(1f));
        gameObject.transform.localScale = new Vector3(0.2194755f, 0.2194755f, 0.2194755f);
    }


    IEnumerator Stall(float timeStalled)
    {
        yield return new WaitForSeconds(timeStalled);
    }
}
