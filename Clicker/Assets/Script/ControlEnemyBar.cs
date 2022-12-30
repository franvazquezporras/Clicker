using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlEnemyBar : MonoBehaviour
{
    [SerializeField] private Image lifeBar;
    [SerializeField] private Text lifeBarText;
    GameObject enemy;
    [SerializeField] private GameObject[] enemies;
    private float maxLife;
    private void Update()
    {
        if(enemy == null)
        {
            enemy = Instantiate(enemies[Random.Range(0, 4)]);
            maxLife = enemy.GetComponent<EnemyControl>().GetEnemyLife();
        }
        else
        {
            lifeBar.fillAmount = enemy.GetComponent<EnemyControl>().GetEnemyLife() /maxLife;
            lifeBarText.text = "Vida: "+ enemy.GetComponent<EnemyControl>().GetEnemyLife();
            if (enemy.GetComponent<EnemyControl>().GetEnemyLife() <= 0)
            {
                Destroy(enemy);
                enemy = null;
            }
        }

    }

    public GameObject GetEnemy()
    {
        return enemy;
    }
  
   
}
