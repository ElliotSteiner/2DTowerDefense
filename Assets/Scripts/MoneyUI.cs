using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{

    public TMP_Text coins;
    public TMP_Text gems;
    public TMP_Text shopGems;

    void Update()
    {
        coins.text = EnemyDeath.money.ToString();
        gems.text = EnemyDeath.gems.ToString();
        shopGems.text = EnemyDeath.gems.ToString();
    }
}
