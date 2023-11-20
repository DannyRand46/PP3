using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Trace : MonoBehaviour
{
    [SerializeField] float timer;
    GameObject target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        string targetTag = GameManager.instance.player.GetComponent<Tracker>().GetTag();
        target = GameObject.FindWithTag(targetTag);
        agent = gameObject.GetComponent<NavMeshAgent>();
        Destroy(gameObject, timer);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject != null)
        {
            if (agent.isActiveAndEnabled && target != null)
            {
                agent.SetDestination(target.transform.position);
            }
        }
    }
}
