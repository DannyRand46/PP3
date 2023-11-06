using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSpawn : MonoBehaviour
{
    [SerializeField] GameObject maze;
    [SerializeField] GameObject combat;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag("CombatTransition") == null)
        {
            maze.GetComponent<RecursiveDepthFirstSearch>().SetItem(combat);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
