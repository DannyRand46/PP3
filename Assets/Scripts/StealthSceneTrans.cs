using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StealthSceneTrans : MonoBehaviour
{
    [SerializeField] GameObject interact;

    bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetButtonDown("Interact"))
        {
            for(int i = 0; i < MazeState.instance.items1.Count; i++)
            {
                if (MazeState.instance.items1[i].item.CompareTag("Stealth"))
                {
                    RecursiveDepthFirstSearch.ChangeSpawn(MazeState.instance.items1[i].tile);
                    break;
                }
            }
            PlayerSaveState.instance.Save();
            SceneManager.LoadScene("Stealth");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.isTrigger)
        {
            return;
        }

        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            interact.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interact.SetActive(false);
        }
    }
}
