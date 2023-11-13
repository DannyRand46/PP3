using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMaze : MonoBehaviour
{
    [SerializeField] GameObject button;
    [SerializeField] FadeToBlack fade;

    bool doorcheck;

    bool playerInRange;
    // Start is called before the first frame update
    void Start()
    {
        doorcheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetButtonDown("Interact"))
        {
            if (fade != null)
            {
                fade.fading();
                doorcheck = true;
                Currency.instance.GainDrachma(50);
                PlayerSaveState.instance.Save();
            }
        }
        if (fade.f == false && doorcheck)
        {

            SceneManager.LoadScene("ProtoType2");
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
