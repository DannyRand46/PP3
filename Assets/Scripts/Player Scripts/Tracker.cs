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
    [SerializeField] public int manaCost;
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
            if (obj != null)
            {
                if (GameManager.instance.playerScript.ConsumeMana(manaCost))
                {
                    Instantiate(tracker, gameObject.transform.position, Quaternion.identity);
                }
            }
        }

        if(Input.GetButtonDown("Tab"))
        {
            //Cycle through trackable items
            objIms[index].gameObject.SetActive(false);
            if(index == objIms.Count - 1)
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

    public string GetTag()
    {
        string output;
        if(index == 0 && !MazeState.instance.mini1)
        {
            output = "NecroTransition";
        }
        else if(index == 0 && !MazeState.instance.mini2)
        {
            output = "GolemTransition";
        }
        else
        {
            output = objs[index].tag;
        }
        return output;
    }
}
