using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource punch;
    private float enemyLife = 10;
    private GameManager gm;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        punch = GetComponent<AudioSource>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        enemyLife = (enemyLife*gm.GetBasicDamage() +( gm.GetPoisonDamage() + gm.GetSpecialDamage()))+gm.GetEnemyKilled();
    }

    private void Start()
    {
        StartCoroutine(PoisonDamageGetting());        
    }
    private void OnMouseDown()
    {
        anim.SetBool("Hit", true);
        punch.Play();
        StartCoroutine(StopHit());
        if (gm.GetIsSpecialDamageActive())
            SetEnemyLife(gm.GetBasicDamage() + gm.GetSpecialDamage());
        else
            SetEnemyLife(gm.GetBasicDamage());
    }
    private void OnDestroy()
    {
        gm.SetenemyKilled();
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
