using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hit;    
    [SerializeField] private AudioSource audioDeath;
    [SerializeField] private float enemyLife;
    private GameManager gm;
    private bool timeOutEnemy = false;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        enemyLife = (enemyLife+(gm.GetBasicDamage() * gm.GetPoisonDamage() * gm.GetSpecialDamage())+(gm.GetEnemyKilled()-gm.GetLevelReduce()));
        audioDeath = GameObject.FindGameObjectWithTag("audioDeath").GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(PoisonDamageGetting());        
    }
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
    public GameManager GetGM() { return gm; }
    public float GetEnemyLife() { return enemyLife; }
    public void SetEnemyLife(float dmg,bool timeOut) { enemyLife -= dmg; timeOutEnemy = timeOut; }
    private IEnumerator StopHit()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Hit", false);
    }
    private IEnumerator PoisonDamageGetting()
    {        
        while (enemyLife > 0)
        {           
            yield return new WaitForSeconds(1);
            enemyLife -= gm.GetPoisonDamage();            
        }        
    }
 
}
