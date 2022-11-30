using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{

    public TMP_Text coins;

    void Update()
    {
        coins.text = EnemyDeath.money.ToString();
    }
}
