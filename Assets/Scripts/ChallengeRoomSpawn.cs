using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeRoomSpawn : MonoBehaviour
{

    [SerializeField] GameObject maze;
    [SerializeField] GameObject challengeHatch;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag("WaterRoom") == null)
        {
            maze.GetComponent<RecursiveDepthFirstSearch>().SetItem(challengeHatch);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
