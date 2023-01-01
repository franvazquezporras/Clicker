using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    //Variables
    [Header("TimerUI")]
    [SerializeField] private Image uiFill;
    [SerializeField] private Text uiText;
    [SerializeField] private Text reducedLevelText;

    [Header("Level Time")]
    public int duration;
    private int remainingDuration;

    [Header("References")]
    [SerializeField] private ControlEnemyBar enemysSpawned;

    /*********************************************************************************************************************************/
    /*Funcion: Start                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Envia la duracion del timer para iniciar el timer                                                                 */
    /*********************************************************************************************************************************/
    private void Start()
    {
        Being(duration);
    }

    /*********************************************************************************************************************************/
    /*Funcion: Update                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Controla cuando el destruido se resetee el tiempo                                                                 */
    /*********************************************************************************************************************************/
    private void Update()
    {
        if (enemysSpawned.GetEnemy() == null)
            SetExtraTime();     
    }

    /*********************************************************************************************************************************/
    /*Funcion: Being                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: second (numero de segundos con los que se comenzara el nivel)                                           */
    /*Descripción: Inicia el reloj a traves de la coroutine UpdateTimer                                                              */
    /*********************************************************************************************************************************/
    private void Being(int second)
    {
        remainingDuration = second;
        StartCoroutine(UpdateTimer());
    }
    /*********************************************************************************************************************************/
    /*Funcion: EndTimer                                                                                                              */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Carga el panel de perder el nivel                                                                                 */
    /*********************************************************************************************************************************/
    private void EndTimer()
    {
        enemysSpawned.GetEnemy().GetComponent<EnemyControl>().GetGM().SetLevelReduce();        
        enemysSpawned.GetEnemy().GetComponent<EnemyControl>().SetEnemyLife(enemysSpawned.GetEnemy().GetComponent<EnemyControl>().GetEnemyLife(),true);
        SetExtraTime();
        StartCoroutine(ReduceLevel());
        StartCoroutine(UpdateTimer());
    }

    /*********************************************************************************************************************************/
    /*Funcion: SetExtraTime                                                                                                          */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Resetea el tiempo al eliminar un enemigo                                                                          */
    /*********************************************************************************************************************************/
    public void SetExtraTime()
    {
        remainingDuration = 30;        
    }

    /*********************************************************************************************************************************/
    /*Funcion: UpdateText                                                                                                            */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Actualiza el  temporizador de la pantalla                                                                         */
    /*********************************************************************************************************************************/
    private void UpdateText()
    {
        uiText.text = $"{remainingDuration / 60:00} : {remainingDuration % 60:00}";
        uiFill.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration);
    }

    /*********************************************************************************************************************************/
    /*Funcion: ReduceLevel                                                                                                           */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Muestra el texto de nivel reducido cuando el tiempo llega a 0                                                     */
    /*********************************************************************************************************************************/
    private IEnumerator ReduceLevel()
    {
        reducedLevelText.text = "Reduced level: -"+ enemysSpawned.GetEnemy().GetComponent<EnemyControl>().GetGM().GetLevelReduce();
        reducedLevelText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        reducedLevelText.gameObject.SetActive(false);
    }
    /*********************************************************************************************************************************/
    /*Funcion: UpdateTimer                                                                                                           */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Actualiza el tiempo y la barra de tiempo controlando cuando llegue a 0                                            */
    /*********************************************************************************************************************************/
    private IEnumerator UpdateTimer()
    {
        //yield return new WaitForSeconds(3);
        while (remainingDuration >= 0)
        {
            UpdateText();
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }
        EndTimer();
    }
}
