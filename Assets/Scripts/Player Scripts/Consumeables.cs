using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Consumeables : MonoBehaviour
{
    [SerializeField] TMP_Text potCount;
    [SerializeField] int hpRestore;

    int pots;

    // Start is called before the first frame update
    void Start()
    {
        //Set pot count to saved amount

    }

    public void GainPot()
    {
        pots++;
        potCount.text = pots.ToString("F0");
    }

    public void UsePot()
    {
        if (pots > 0)
        {
            GameManager.instance.player.GetComponent<PlayerController>().AddHealth(hpRestore);
            pots--;
            potCount.text = pots.ToString("F0");
        }
    }
}
