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
    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        enemyLife = (enemyLife*gm.GetBasicDamage() +( gm.GetPoisonDamage() + gm.GetSpecialDamage()))+gm.GetEnemyKilled();
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
            SetEnemyLife(gm.GetBasicDamage() + gm.GetSpecialDamage());
        else
            SetEnemyLife(gm.GetBasicDamage());
        
    }
    private void OnDestroy()
    {
        if(audioDeath !=null)
            audioDeath.Play();
        if (enemyLife <= 0)
        {
            gm.SetenemyKilled();
            gm.SetPlayerCoin((int)(gm.GetBasicDamage() + gm.GetPoisonDamage() + gm.GetSpecialDamage() + gm.GetEnemyKilled()));          
        }
        StopAllCoroutines();        
    }
    public float GetEnemyLife() { return enemyLife; }
    public void SetEnemyLife(float dmg) { enemyLife -= dmg; }
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
