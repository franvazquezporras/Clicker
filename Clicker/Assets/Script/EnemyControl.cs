using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private Animator anim;
    private AudioSource punch;
    private int EnemyLife;
    private GameManager gm;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        punch = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        anim.SetBool("Hit", true);
        punch.Play();
        StartCoroutine(StopHit());
    }
    

    private IEnumerator StopHit()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Hit", false);
    }
}
