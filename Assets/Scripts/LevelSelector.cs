using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class LevelSelector : MonoBehaviour

   
{
    public int level;
    EnemyDeath enemyDeath;
    // Start is called before the first frame update
    void Start()
    {
        enemyDeath = new EnemyDeath();
    }

    public void OpenScene()
    {
        SceneManager.LoadScene("Level " + level.ToString());
        level++;
        enemyDeath.StartMoney(300);
    }

}
