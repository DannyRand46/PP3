using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroSpawn : MonoBehaviour
{
    [SerializeField] GameObject maze;
    [SerializeField] GameObject miniBoss;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag("NecroTransition") == null)
        {
            maze.GetComponent<RecursiveDepthFirstSearch>().SetItem(miniBoss);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
