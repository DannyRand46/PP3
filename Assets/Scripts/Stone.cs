using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] int damage;
    [SerializeField] float speed;
    [SerializeField] int timeToDestroy;

    private GameObject thrower;
    public AudioClip stoneAudio;

    public void SetThrower(GameObject thrower)
    {
        this.thrower = thrower;
    }

    private void Start()
    {
        rb.velocity = (GameManager.instance.player.transform.position - transform.position).normalized * speed;
        Destroy(gameObject, timeToDestroy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        IDamage canDamage = other.GetComponent<IDamage>();

        if (canDamage != null) 
        {
            canDamage.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
