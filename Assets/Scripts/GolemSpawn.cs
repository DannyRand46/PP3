using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSpawn : MonoBehaviour
{
    [SerializeField] GameObject maze;
    [SerializeField] GameObject gol;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag("GolemTransition") == null)
        {
            maze.GetComponent<RecursiveDepthFirstSearch>().SetItem(gol);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
