using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Tracker : MonoBehaviour
{
    [Header("----- Components -----")]
    [SerializeField] GameObject tracker;
    [SerializeField] List<GameObject> objs = new List<GameObject>();
    [SerializeField] List<Image> objIms = new List<Image>();

    int index;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Trace"))
        {
            //If object exists in scene, track to it
            GameObject obj = GameObject.FindWithTag(objs[index].tag);
            if(obj != null)
            {
                Instantiate(tracker, gameObject.transform.position, Quaternion.identity);
            }
        }

        if(Input.GetButtonDown("Tab"))
        {
            //Cycle through trackable items
            objIms[index].gameObject.SetActive(false);
            if(index == 2)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            objIms[index].gameObject.SetActive(true);
        }
    }


    public GameObject GetCurrSel()
    {
        return GameObject.FindWithTag(objs[index].tag);
    }
}
