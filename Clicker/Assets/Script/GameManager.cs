using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private int playerCoins = 100;
    private float basicDamage = 1;
    private float poisonDamage = 0;
    private float specialDamage = 0;
    private int enemyKilled = 0;
    private bool isSpecialDamageActive;
    private bool coldDown;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isSpecialDamageActive && !coldDown)
        {
            SetIsSpecialDamageActive();
            StartCoroutine(ActiveSpecialDamage());
        }            
    }
    public int GetPlayerCoins() { return playerCoins; }
    public float GetBasicDamage() { return basicDamage; }
    public float GetPoisonDamage() { return poisonDamage; }
    public float GetSpecialDamage() { return specialDamage; }
    public int GetEnemyKilled() { return enemyKilled; }
    public bool GetIsSpecialDamageActive() { return isSpecialDamageActive; }
    public void SetBasicDamage(float damage) { basicDamage += damage; }
    public void SetPoisonDamage(float damage) { poisonDamage += damage; }
    public void SetSpecialDamage(float damage) { specialDamage += damage; }
    public void SetPlayerCoin(int coin) { playerCoins += coin; }    
    public void SetenemyKilled() { enemyKilled++; }
    public void SetIsSpecialDamageActive() { isSpecialDamageActive = !isSpecialDamageActive; }

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
