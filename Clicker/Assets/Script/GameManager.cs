using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private int playerCoins;
    private float basicDamage;
    private float poisonDamage;
    private float specialDamage;
    private int enemyKilled;
    private bool isSpecialDamageActive;
    private bool coldDown;

    private void Awake()
    {
        playerCoins = PlayerPrefs.GetInt("playerCoins");        
        basicDamage = PlayerPrefs.GetFloat("basicDamage") != 0 ? PlayerPrefs.GetFloat("basicDamage") : 1;
        poisonDamage = PlayerPrefs.GetFloat("poisonDamage");
        specialDamage = PlayerPrefs.GetFloat("specialDamage");
        enemyKilled = PlayerPrefs.GetInt("enemyLevel");
    }
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
    public void SetBasicDamage(float damage) { 
        basicDamage += damage;
        PlayerPrefs.SetFloat("basicDamage", basicDamage);
    }
    public void SetPoisonDamage(float damage) { 
        poisonDamage += damage;
        PlayerPrefs.SetFloat("poisonDamage", poisonDamage);
    }
    public void SetSpecialDamage(float damage) { 
        specialDamage += damage;
        PlayerPrefs.SetFloat("specialDamage", specialDamage);
    }
    public void SetPlayerCoin(int coin) { 
        playerCoins += coin;       
        PlayerPrefs.SetInt("playerCoins", playerCoins);        
    }    
    public void SetenemyKilled() { 
        enemyKilled++;
        PlayerPrefs.SetInt("enemyLevel", enemyKilled);
    }
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
