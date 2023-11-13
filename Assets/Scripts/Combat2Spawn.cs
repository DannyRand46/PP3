using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat2Spawn : MonoBehaviour
{
    [SerializeField] GameObject maze;
    [SerializeField] GameObject com2;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag("Combat2Transition") == null)
        {
            maze.GetComponent<RecursiveDepthFirstSearch>().SetItem(com2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
