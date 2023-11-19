using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public static Currency instance;

    public GameObject currDisplay;
    public TMP_Text currText;
    public bool activeDis;

    int drachma;

    // Start is called before the first frame update
    void Awake()
    {
        //Create instance if not existant already
        if (instance == null)
        {
            instance = this;
        }
        //Destroy this instance if there is already one in existance
        else if (instance != this)
        {
            if (currDisplay != null)
            {
                instance.SetDisplay();
            }
            Destroy(gameObject);
        }

        drachma = 0;
        activeDis = false;

        //Keeps this object as instanced for tracking info between scenes
        DontDestroyOnLoad(gameObject);
    }

    //Gives amount of drachma to player
    public void GainDrachma(int amount)
    {
        drachma += amount;
        currText.text = drachma.ToString("F0");
        if(currDisplay != null && !activeDis)
        {
            SetDisplay();
        }
    }

    //Spends amount of drachma given (must be error checked before this function is called)
    public void SpendDrachma(int amount)
    {
        drachma -= amount;
        currText.text = drachma.ToString("F0");
    }

    //Resets money on respawn
    public void ResetDrachma()
    {
        drachma = 50;
    }

    public void SetDisplay()
    {
        currDisplay.SetActive(true);
        activeDis = true;
    }

    //Getter for drachma count
    public int GetDrachma()
    {
        return drachma;
    }
}
