using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    [Header("----- Components -----")]
    [SerializeField] GameObject tracker;
    [SerializeField] List<GameObject> objs = new List<GameObject>();

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
            //Open item selection menu

        }
    }


    public GameObject GetCurrSel()
    {
        return GameObject.FindWithTag(objs[index].tag);
    }
}
