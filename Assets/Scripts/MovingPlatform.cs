using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float speed = 2.0f;
    
    public Transform[] points;
    public int currentPointIndex = 0;
    public bool isActive = false;

    private Vector3 previousPosition;
    private Transform playerTransform;

    private void Start()
    {
        previousPosition = transform.position;
    }

    private void Update()
    {
        if (!isActive || points.Length == 0) return;

        Vector3 platformDeltaPosition = transform.position - previousPosition;
        MoveNext();

        if (playerTransform != null)
        {
            
            playerTransform.position += platformDeltaPosition;
        }

        previousPosition = transform.position;
    }

    private void MoveNext()
    {
        Transform targetPoint = points[currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (transform.position == targetPoint.position)
        {
            currentPointIndex++;
            if (currentPointIndex >= points.Length)
            {
                currentPointIndex = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = collision.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = null;
        }
    }

    public void ActivatePlatform()
    {
        isActive = true;
    }
}
