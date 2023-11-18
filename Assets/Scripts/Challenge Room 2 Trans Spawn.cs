using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeRoom2TransSpawn : MonoBehaviour
{
    [SerializeField] GameObject maze;
    [SerializeField] GameObject chall;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag("CubeRoom") == null)
        {
            maze.GetComponent<RecursiveDepthFirstSearch>().SetItem(chall);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
