using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public WaterControl waterRising;
    public MovingPlatform[] platforms;
    public GameObject exitDoor;
    private AudioSource audioSource;
    public GameObject leverHandle;

    private bool isPlayerNear = false;
    private bool isLeverActivated = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (isPlayerNear && !isLeverActivated && Input.GetKeyDown(KeyCode.E))
        {
            ActivateLever();
            audioSource.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    private void ActivateLever()
    {
        if (waterRising != null)
        {
            Destroy(exitDoor);
            waterRising.StartRising();
            MoveLeverHandle(); 

            foreach (var platform in platforms)
            {
                platform.ActivatePlatform();
            }
        }

       
        isLeverActivated = true;
    }

    private void MoveLeverHandle()
    {
        leverHandle.transform.Rotate(new Vector3(45, 0, 0));
    }
}
