using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLever : MonoBehaviour
{
    public GameObject controlledCube;
    public float rotationDuration = 1.5f;
    private bool isPlayerNear = false;
    private bool isRotating = false;
    public CubePuzzle currentCombination;

    private void Update()
    {
        if(isPlayerNear && Input.GetKeyDown(KeyCode.E) && !isRotating)
        {
            StartCoroutine(RotateCube(90f, rotationDuration));
        }

    }

    IEnumerator RotateCube(float angle, float duration)
    {
        currentCombination.StartTimer();
        isRotating = true;
        Quaternion originalRotation = controlledCube.transform.rotation;
        Quaternion finalRotation = controlledCube.transform.rotation * Quaternion.Euler(0, angle, 0);
        float elapsedTime = 0;

        while(elapsedTime < duration) 
        {
            controlledCube.transform.rotation = Quaternion.Lerp(originalRotation, finalRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        controlledCube.transform.rotation = finalRotation;
        isRotating = false;
        currentCombination.CheckPuzzle();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
       if(other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
