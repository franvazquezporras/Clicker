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
    private void FixedUpdate()
    {
        
    }
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
       //PERDER
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



    private void UpdateText()
    {
        uiText.text = $"{remainingDuration / 60:00} : {remainingDuration % 60:00}";
        uiFill.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration);
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
