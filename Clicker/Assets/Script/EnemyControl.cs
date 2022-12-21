using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        anim.SetBool("Hit", true);
        
    }
    private void OnMouseUp()
    {
        anim.SetBool("Hit", false);
    }
}
