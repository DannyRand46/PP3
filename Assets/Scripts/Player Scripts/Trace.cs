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
        target = GameManager.instance.player.GetComponent<Tracker>().GetCurrSel();
        agent = gameObject.GetComponent<NavMeshAgent>();
        Destroy(gameObject, timer);
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.isActiveAndEnabled)
        {
            agent.SetDestination(target.transform.position);
        }
    }
}
