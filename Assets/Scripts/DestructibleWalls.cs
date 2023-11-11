using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWalls : MonoBehaviour
{
    [SerializeField] GameObject brokenWallPrefab;
    [Range(1, 10)][SerializeField] float magnitude;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BreakWall(Vector3 explosionDirection)
    {
        Transform t = transform;
        BoxCollider collider = t.GetComponent<BoxCollider>();
        collider.enabled = false;
        GameObject wall = Instantiate(brokenWallPrefab, t.position, t.rotation);
        Rigidbody[] wallChildren = wall.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < wallChildren.Length; i++)
        {
            wallChildren[i].AddForce(explosionDirection * magnitude, ForceMode.Impulse);
        }
        Destroy(gameObject);
    }
}
