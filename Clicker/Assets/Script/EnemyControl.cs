using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    //Variables
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hit;    
    [SerializeField] private AudioSource audioDeath;
    [SerializeField] private float enemyLife;
    private GameManager gm;
    private bool timeOutEnemy = false;

    /*********************************************************************************************************************************/
    /*Funcion: Awake                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripci�n: Referencias de las variables a los componentes                                                                    */
    /*********************************************************************************************************************************/
    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        enemyLife = (enemyLife+(gm.GetBasicDamage() + gm.GetPoisonDamage())+ gm.GetSpecialDamage())*(gm.GetEnemyKilled()-gm.GetLevelReduce());
        audioDeath = GameObject.FindGameObjectWithTag("audioDeath").GetComponent<AudioSource>();
    }

    /*********************************************************************************************************************************/
    /*Funcion: Start                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripci�n: Inicia el da�o por veneno                                                                                         */
    /*********************************************************************************************************************************/
    private void Start()
    {
        StartCoroutine(PoisonDamageGetting());        
    }

    /*********************************************************************************************************************************/
    /*Funcion: OnMouseDown                                                                                                           */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripci�n: Controla cuando el jugador a clicado encima del enemigo efectuando el sonido de golpeo y quitando vida al enemigo */
    /*********************************************************************************************************************************/
    private void OnMouseDown()
    {
        anim.SetBool("Hit", true);
        audioSource.clip = hit;
        audioSource.Play();
        StartCoroutine(StopHit());
        if (gm.GetIsSpecialDamageActive())
            SetEnemyLife(gm.GetBasicDamage() + gm.GetSpecialDamage(),false);
        else
            SetEnemyLife(gm.GetBasicDamage(),false);        
    }

    /*********************************************************************************************************************************/
    /*Funcion: OnDestroy                                                                                                             */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripci�n: Controla cuando el enemigo es eliminado para sumar monedas y contador de enemigos muertos                         */
    /*********************************************************************************************************************************/
    private void OnDestroy()
    {
        if(audioDeath !=null)
            audioDeath.Play();
        if (enemyLife <= 0 && !timeOutEnemy)
        {
            gm.SetenemyKilled(1);
            gm.SetPlayerCoin((int)(gm.GetBasicDamage() + gm.GetPoisonDamage() + gm.GetSpecialDamage() + gm.GetEnemyKilled()));          
        }
        StopAllCoroutines();        
    }
    /*********************************************************************************************************************************/
    /*Funcion: GetGM                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripci�n: Devuelve el valor de GameManager                                                                                  */
    /*********************************************************************************************************************************/
    public GameManager GetGM() { return gm; }


    /*********************************************************************************************************************************/
    /*Funcion: GetEnemyLife                                                                                                          */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripci�n: Devuelve la vida del enemigo                                                                                      */
    /*********************************************************************************************************************************/
    public float GetEnemyLife() { return enemyLife; }

    /*********************************************************************************************************************************/
    /*Funcion: SetEnemyLife                                                                                                          */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripci�n: Modifica la vida del enemigo                                                                                      */
    /*********************************************************************************************************************************/
    public void SetEnemyLife(float dmg,bool timeOut) { enemyLife -= dmg; timeOutEnemy = timeOut; }

    /*********************************************************************************************************************************/
    /*Funcion: StopHit                                                                                                               */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripci�n: Corrutina para finalizar la animacion del enemigo golpeado                                                        */
    /*********************************************************************************************************************************/
    private IEnumerator StopHit()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Hit", false);
    }

    /*********************************************************************************************************************************/
    /*Funcion: PoisonDamageGetting                                                                                                   */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripci�n: Da�o por segundo realizado por el veneno                                                                          */
    /*********************************************************************************************************************************/
    private IEnumerator PoisonDamageGetting()
    {        
        while (enemyLife > 0)
        {           
            yield return new WaitForSeconds(1);
            enemyLife -= gm.GetPoisonDamage();            
        }        
    }
 
}
