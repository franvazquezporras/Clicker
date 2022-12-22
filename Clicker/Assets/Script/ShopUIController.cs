using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI basicAttackText;
    [SerializeField] private TextMeshProUGUI poisonAttackText;
    [SerializeField] private TextMeshProUGUI specialAttackText;
    [SerializeField] private TextMeshProUGUI playerCoins;
    [SerializeField] private TextMeshProUGUI priceBasicAttackText;
    [SerializeField] private TextMeshProUGUI pricePoisonAttackText;
    [SerializeField] private TextMeshProUGUI priceSpecialAttackText;
    private int priceBasicAttack = 5;
    private int pricePoisonAttack = 5;
    private int priceSpecialAttack = 5;
    private GameManager gm;

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        basicAttackText.text = gm.GetBasicDamage().ToString();
        poisonAttackText.text = gm.GetPoisonDamage().ToString();
        specialAttackText.text = gm.GetSpecialDamage().ToString();
        playerCoins.text = "Monedas: " + gm.GetPlayerCoins();
        priceBasicAttackText.text = "Precio: " + priceBasicAttack;
        pricePoisonAttackText.text = "Precio: " + pricePoisonAttack;
        priceSpecialAttackText.text = "Precio: " + priceSpecialAttack;
    }


    public void BuyBasicDamage()
    {
        if (gm.GetPlayerCoins()- priceBasicAttack >= 0 )
        {
            gm.SetBasicDamage(1);
            priceBasicAttack += 5;
            gm.SetPlayerCoin(-priceBasicAttack);
        }            
    }
    public void BuyPoisonDamage()
    {
        if (gm.GetPlayerCoins() - pricePoisonAttack >= 0)
        {
            gm.SetPoisonDamage(1);
            pricePoisonAttack += 5;
            gm.SetPlayerCoin(-pricePoisonAttack);
        }            
    }
    public void BuySpecialDamage()
    {
        if (gm.GetPlayerCoins()- priceSpecialAttack >= 0)
        {
            gm.SetSpecialDamage(1);
            priceSpecialAttack += 5;
            gm.SetPlayerCoin(-priceSpecialAttack);
        }            
    }
}
