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

    /*********************************************************************************************************************************/
    /*Funcion: Awake                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */    
    /*Descripción: Referencias de textos y GameManager                                                                               */
    /*********************************************************************************************************************************/
    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        priceBasicAttack =5*(int)PlayerPrefs.GetFloat("basicDamage") +5;
        pricePoisonAttack =5* (int)PlayerPrefs.GetFloat("poisonDamage") +5;
        priceSpecialAttack =5* (int)PlayerPrefs.GetFloat("specialDamage")+5;
       
    }

    /*********************************************************************************************************************************/
    /*Funcion: Update                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Actualiza los textos de la tienda                                                                                 */
    /*********************************************************************************************************************************/
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

    /*********************************************************************************************************************************/
    /*Funcion: BuyBasicDamage                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Aumenta el daño basico del jugador,resta el precio del dinero del jugador y aumenta el precio del siguiente nivel */
    /*********************************************************************************************************************************/
    public void BuyBasicDamage()
    {
        if (gm.GetPlayerCoins()- priceBasicAttack >= 0 )
        {
            gm.SetBasicDamage(1);        
            gm.SetPlayerCoin(-priceBasicAttack);
            priceBasicAttack += 5;
        }            
    }


    /*********************************************************************************************************************************/
    /*Funcion: BuyPoisonDamage                                                                                                       */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Aumenta el daño basico por veneno,resta el precio del dinero del jugador y aumenta el precio del siguiente nivel  */
    /*********************************************************************************************************************************/
    public void BuyPoisonDamage()
    {
        if (gm.GetPlayerCoins() - pricePoisonAttack >= 0)
        {
            gm.SetPoisonDamage(1);            
            gm.SetPlayerCoin(-pricePoisonAttack);
            pricePoisonAttack += 5;
        }            
    }
    /*********************************************************************************************************************************/
    /*Funcion: BuySpecialDamage                                                                                                      */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Aumenta el daño basico especial,resta el precio del dinero del jugador y aumenta el precio del siguiente nivel    */
    /*********************************************************************************************************************************/
    public void BuySpecialDamage()
    {
        if (gm.GetPlayerCoins()- priceSpecialAttack >= 0)
        {
            gm.SetSpecialDamage(1);            
            gm.SetPlayerCoin(-priceSpecialAttack);
            priceSpecialAttack += 5;
        }            
    }
}
