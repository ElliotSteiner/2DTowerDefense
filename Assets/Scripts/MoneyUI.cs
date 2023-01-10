using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{

    public TMP_Text coins;
    public TMP_Text gems;

    void Update()
    {
        gems.text = EnemyDeath.gems.ToString();
        coins.text = EnemyDeath.money.ToString();
    }
}
