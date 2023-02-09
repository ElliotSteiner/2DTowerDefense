using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }

    public Transform pfProjectile;
    public Transform pfTowerWizard;
    public Transform pfTowerArcher;
    public Transform pfTowerCannon;
    public Transform pfTowerBoulder;
    public Transform pfTowerLookout;
    public Transform pfTowerDruid;
    

    //public Transform pf_EnemyMinor;
    //public Transform pf_EnemyMedium;
}
