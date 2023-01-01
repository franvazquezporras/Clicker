using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlEnemyBar : MonoBehaviour
{
    //Variables
    [SerializeField] private Image lifeBar;
    [SerializeField] private Text lifeBarText;
    GameObject enemy;
    [SerializeField] private GameObject[] enemies;
    private float maxLife;
    private int enemyCharacterUnlocked = 3;

    /*********************************************************************************************************************************/
    /*Funcion: Update                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Controla cuando hay un enemigo con vida, actualiza la barra de vida y genera un enemigo nuevo cuando el anterior  */
    /*              es eliminado                                                                                                     */
    /*********************************************************************************************************************************/
    private void Update()
    {
        if(enemy == null)
        {
            enemy = Instantiate(enemies[Random.Range(0, enemyCharacterUnlocked)]);
            maxLife = enemy.GetComponent<EnemyControl>().GetEnemyLife();
        }
        else
        {
            lifeBar.fillAmount = enemy.GetComponent<EnemyControl>().GetEnemyLife() /maxLife;
            lifeBarText.text = "Vida: "+ enemy.GetComponent<EnemyControl>().GetEnemyLife();
            if (enemy.GetComponent<EnemyControl>().GetEnemyLife() <= 0)
            {
                newCharacter();
                Destroy(enemy);
                enemy = null;
            }
        }

    }

    /*********************************************************************************************************************************/
    /*Funcion: GetEnemy                                                                                                              */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Devuelve el valor del enemigo que esta vivo actualmente                                                           */
    /*********************************************************************************************************************************/
    public GameObject GetEnemy()
    {
        return enemy;
    }

    /*********************************************************************************************************************************/
    /*Funcion: newCharacter                                                                                                          */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Desbloquea un nuevo enemigo cuando se han eliminado x enemigos                                                    */
    /*********************************************************************************************************************************/
    private void newCharacter()
    {
        switch (enemy.GetComponent<EnemyControl>().GetGM().GetEnemyKilled())
        {
            case 10:
            case 20:
            case 30:
            case 40:
            case 50:
            case 60:            
                enemyCharacterUnlocked++;
                break;
            default:
                break;
        }
    }
   
}
