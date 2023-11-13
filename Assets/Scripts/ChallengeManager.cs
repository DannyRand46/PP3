using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChallengeManager : MonoBehaviour
{
    private Vector3 playerStartPos;
    private Quaternion playerStartRotation;

    public GameObject player;
    public GameObject waterObject;
    public GameObject loseBarrier;
    
    
    private void Start()
    {
        if(player != null)
        {
            playerStartPos = player.transform.position;
            playerStartRotation = player.transform.rotation;
        }
 

        //Open room
        if(loseBarrier != null)
        {
            loseBarrier.SetActive(false);
        }
    }

    public void CheckChallengeStatus()
    {
        if (waterObject.transform.position.y > player.transform.position.y)
        {
            LoadNewScene();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            LoadNewScene();
        }
    }

    private void LoadNewScene()
    {
        SceneManager.LoadScene("Prototype2");
    }
    public void ResetPlayerState()
    {
        player.transform.position = playerStartPos;
        player.transform.rotation = playerStartRotation;
       
    }

    public void CloseRoom()
    {
        if(loseBarrier != null)
        {
            loseBarrier.SetActive(true);
        }
    }
}
