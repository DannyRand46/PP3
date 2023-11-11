using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPeicesCleanUp : MonoBehaviour
{
    [Range(5, 15)][SerializeField] float timeTillDestroy;
    float destroyTimer;
    // Start is called before the first frame update
    void Start()
    {
        destroyTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        destroyTimer += Time.deltaTime;
        if( destroyTimer >= timeTillDestroy )
        {
            DestroyAllChildren();
            Destroy(gameObject);
        }
    }

    public void DestroyAllChildren()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        for (int i = 0;  i < children.Length; i++)
        {
            Destroy(children[i].gameObject);
        }
    }
}
