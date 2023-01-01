using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private int playerCoins;
    private float basicDamage;
    private float poisonDamage;
    private float specialDamage;
    private int enemyKilled;
    private bool isSpecialDamageActive;
    private bool coldDown;
    private int levelReduce;

    /*********************************************************************************************************************************/
    /*Funcion: Awake                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Carga los valores de los playerpref en las variables                                                              */
    /*********************************************************************************************************************************/
    private void Awake()
    {
        playerCoins = PlayerPrefs.GetInt("playerCoins");        
        basicDamage = PlayerPrefs.GetFloat("basicDamage") != 0 ? PlayerPrefs.GetFloat("basicDamage") : 1;
        poisonDamage = PlayerPrefs.GetFloat("poisonDamage");
        specialDamage = PlayerPrefs.GetFloat("specialDamage");
        enemyKilled = PlayerPrefs.GetInt("enemyLevel");
        levelReduce = PlayerPrefs.GetInt("levelReduce");
        if (enemyKilled - levelReduce <= 0)
            levelReduce = 0;
        
    }


    /*********************************************************************************************************************************/
    /*Funcion: Update                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Controla cuando se activa la habilidad especial de ataque                                                         */
    /*********************************************************************************************************************************/
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isSpecialDamageActive && !coldDown)
        {
            SetIsSpecialDamageActive();
            StartCoroutine(ActiveSpecialDamage());
        }            
    }

    /*********************************************************************************************************************************/
    /*Funcion: GetPlayerCoins                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Devuelve el valor de monedas que tiene el jugador                                                                 */
    /*********************************************************************************************************************************/
    public int GetPlayerCoins() { return playerCoins; }

    /*********************************************************************************************************************************/
    /*Funcion: GetBasicDamage                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Devuelve el valor de ataque basico que tiene el jugador                                                           */
    /*********************************************************************************************************************************/
    public float GetBasicDamage() { return basicDamage; }

    /*********************************************************************************************************************************/
    /*Funcion: GetPoisonDamage                                                                                                       */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Devuelve el valor de daño por veneno que tiene el jugador                                                         */
    /*********************************************************************************************************************************/
    public float GetPoisonDamage() { return poisonDamage; }

    /*********************************************************************************************************************************/
    /*Funcion: GetSpecialDamage                                                                                                      */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Devuelve el valor de daño por ataque especial que tiene el jugador                                                */
    /*********************************************************************************************************************************/
    public float GetSpecialDamage() { return specialDamage; }

    /*********************************************************************************************************************************/
    /*Funcion: GetEnemyKilled                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Devuelve el valor de enemigos eliminados                                                                          */
    /*********************************************************************************************************************************/
    public int GetEnemyKilled() { return enemyKilled; }

    /*********************************************************************************************************************************/
    /*Funcion: GetIsSpecialDamageActive                                                                                              */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Devuelve si la habilidad de ataque especial está activado o no                                                    */
    /*********************************************************************************************************************************/
    public bool GetIsSpecialDamageActive() { return isSpecialDamageActive; }

    /*********************************************************************************************************************************/
    /*Funcion: GetLevelReduce                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Devuelve el valor que reduce la vida del enemigo por cada vez que el contador llega a 0                           */
    /*********************************************************************************************************************************/
    public int GetLevelReduce() { return levelReduce; }


    /*********************************************************************************************************************************/
    /*Funcion: SetLevelReduce                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Actualiza el valor de reduccion de vida del enemigo                                                               */
    /*********************************************************************************************************************************/
    public void SetLevelReduce()
    {
        levelReduce++;
        PlayerPrefs.SetInt("levelReduce", levelReduce);
    }


    /*********************************************************************************************************************************/
    /*Funcion: SetBasicDamage                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada:damage (daño a aumentar)                                                                                 */
    /*Descripción: Actualiza el valor de ataque basico del jugador                                                                   */
    /*********************************************************************************************************************************/
    public void SetBasicDamage(float damage) { 
        basicDamage += damage;
        PlayerPrefs.SetFloat("basicDamage", basicDamage);
    }


    /*********************************************************************************************************************************/
    /*Funcion: SetPoisonDamage                                                                                                       */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada:damage (daño a aumentar)                                                                                 */
    /*Descripción: Actualiza el valor de ataque por veneno del jugador                                                               */
    /*********************************************************************************************************************************/
    public void SetPoisonDamage(float damage) { 
        poisonDamage += damage;
        PlayerPrefs.SetFloat("poisonDamage", poisonDamage);
    }

    /*********************************************************************************************************************************/
    /*Funcion: SetSpecialDamage                                                                                                      */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada:damage (daño a aumentar)                                                                                 */
    /*Descripción: Actualiza el valor de daño por ataque especial del jugador                                                        */
    /*********************************************************************************************************************************/
    public void SetSpecialDamage(float damage) { 
        specialDamage += damage;
        PlayerPrefs.SetFloat("specialDamage", specialDamage);
    }

    /*********************************************************************************************************************************/
    /*Funcion: SetPlayerCoin                                                                                                         */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada:coin (numero de monedas extras)                                                                          */
    /*Descripción: Actualiza el valor de monedas obtenidas                                                                           */
    /*********************************************************************************************************************************/
    public void SetPlayerCoin(int coin) { 
        playerCoins += coin;       
        PlayerPrefs.SetInt("playerCoins", playerCoins);        
    }

    /*********************************************************************************************************************************/
    /*Funcion: SetenemyKilled                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada:enemy (numero enemigos)                                                                                  */
    /*Descripción: Actualiza el valor de enemigos eliminados                                                                         */
    /*********************************************************************************************************************************/
    public void SetenemyKilled(int enemy) { 
        enemyKilled+= enemy;
        PlayerPrefs.SetInt("enemyLevel", enemyKilled);
    }


    /*********************************************************************************************************************************/
    /*Funcion: SetIsSpecialDamageActive                                                                                              */
    /*Desarrollador: Vazquez                                                                                                         */    
    /*Descripción: Activa o desactiva el valor de habilidad especial                                                                 */
    /*********************************************************************************************************************************/
    public void SetIsSpecialDamageActive() { isSpecialDamageActive = !isSpecialDamageActive; }

    /*********************************************************************************************************************************/
    /*Funcion: ActiveSpecialDamage                                                                                                   */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Controla el tiempo que esta activa la habilidad especial y el coldDown de la misma                                */
    /*********************************************************************************************************************************/
    private IEnumerator ActiveSpecialDamage() {
        
        Debug.Log("SpecialON");
        yield return new WaitForSeconds(8);
        Debug.Log("SpecialOF");
        coldDown = true;
        SetIsSpecialDamageActive();
        yield return new WaitForSeconds(10);
        Debug.Log("SpecialUp");
        coldDown = false;
    }
}
