using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{
    [SerializeField] GameObject currDisplay;

    // Start is called before the first frame update
    void Start()
    {
        if(Currency.instance != null)
        {
            currDisplay.SetActive(Currency.instance.activeDis);
        }
    }
}
