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

    public int GetPlayerCoins() { return playerCoins; }
    public float GetBasicDamage() { return basicDamage; }
    public float GetPoisonDamage() { return poisonDamage; }
    public float GetSpecialDamage() { return specialDamage; }
    public int GetEnemyKilled() { return enemyKilled; }

    public void SetBasicDamage(float damage) { basicDamage += damage; }
    public void SetPoisonDamage(float damage) { poisonDamage += damage; }
    public void SetSpecialDamage(float damage) { specialDamage += damage; }
    public void SetPlayerCoin(int coin) { playerCoins += coin; }    
    public void SetenemyKilled() { enemyKilled--; }
}
