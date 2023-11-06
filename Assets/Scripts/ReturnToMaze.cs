using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMaze : MonoBehaviour
{
    [SerializeField] GameObject button;

    bool playerInRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && Input.GetButtonDown("Interact"))
        {
            SceneManager.LoadScene("Prototype2");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            button.SetActive(true);
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
            button.SetActive(false);
        }
    }
}
