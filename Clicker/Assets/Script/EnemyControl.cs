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
    /*Descripción: Referencias de las variables a los componentes                                                                    */
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
    /*Descripción: Inicia el daño por veneno                                                                                         */
    /*********************************************************************************************************************************/
    private void Start()
    {
        StartCoroutine(PoisonDamageGetting());        
    }

    /*********************************************************************************************************************************/
    /*Funcion: OnMouseDown                                                                                                           */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Controla cuando el jugador a clicado encima del enemigo efectuando el sonido de golpeo y quitando vida al enemigo */
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
    /*Descripción: Controla cuando el enemigo es eliminado para sumar monedas y contador de enemigos muertos                         */
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
    /*Descripción: Devuelve el valor de GameManager                                                                                  */
    /*********************************************************************************************************************************/
    public GameManager GetGM() { return gm; }


    /*********************************************************************************************************************************/
    /*Funcion: GetEnemyLife                                                                                                          */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Devuelve la vida del enemigo                                                                                      */
    /*********************************************************************************************************************************/
    public float GetEnemyLife() { return enemyLife; }

    /*********************************************************************************************************************************/
    /*Funcion: SetEnemyLife                                                                                                          */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Modifica la vida del enemigo                                                                                      */
    /*********************************************************************************************************************************/
    public void SetEnemyLife(float dmg,bool timeOut) { enemyLife -= dmg; timeOutEnemy = timeOut; }

    /*********************************************************************************************************************************/
    /*Funcion: StopHit                                                                                                               */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Corrutina para finalizar la animacion del enemigo golpeado                                                        */
    /*********************************************************************************************************************************/
    private IEnumerator StopHit()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Hit", false);
    }

    /*********************************************************************************************************************************/
    /*Funcion: PoisonDamageGetting                                                                                                   */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Daño por segundo realizado por el veneno                                                                          */
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
