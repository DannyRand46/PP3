using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Display : MonoBehaviour
{
    [SerializeField] GameObject currDis;
    [SerializeField] TMP_Text currT;

    // Start is called before the first frame update
    void Start()
    {
        if(Currency.instance != null)
        {
            GameObject currSys = GameObject.FindWithTag("Currency");
            if(currSys != null)
            {
                currSys.GetComponent<Currency>().currDisplay = currDis;
                currSys.GetComponent<Currency>().currText = currT;
            }
            currDis.SetActive(Currency.instance.activeDis);
        }
    }
}
