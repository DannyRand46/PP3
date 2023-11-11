using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StealthPurch : MonoBehaviour
{
    [SerializeField] TMP_Text dia;
    [SerializeField] GameObject item1;
    [SerializeField] GameObject item2;

    // Start is called before the first frame update
    void Start()
    {
        //Set items based on state of shop inventory
        item1.SetActive(StealthShop.Invis);
        item2.SetActive(StealthShop.Speed);
    }

    public void BuyItem1()
    {
        if (Currency.instance.GetDrachma() >= 80)
        {
            //Add first purchaseable limited quantity item

        }
        else
        {
            dia.text = "Thou dost not possess the currency for that item";
        }
    }

    public void BuyItem2()
    {
        if(Currency.instance.GetDrachma() >= 30)
        {
            //Add second purchaseable limited quantity item

        }
        else
        {
            dia.text = "Thou dost not possess the currency for that item";
        }
    }

    public void BuyPot()
    {
        if(Currency.instance.GetDrachma() >= 10)
        {
            Currency.instance.SpendDrachma(10);
            //Add potion

        }
        else
        {
            dia.text = "Thou dost not possess the currency for a potion";
        }
    }
}
