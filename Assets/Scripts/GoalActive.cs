using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalActive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(MazeState.instance.mini1 &&  MazeState.instance.mini2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
