using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSpawnPoint : MonoBehaviour
{
    public static Transform spawnPoint;
    void Start()
    {
        spawnPoint = GetComponent<Transform>();
    }
}
