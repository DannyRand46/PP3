using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMazeTransition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Currency.instance.GainDrachma(50);
            PlayerSaveState.instance.Save();
            SceneManager.LoadScene("ProtoType2");
            
        }
    }
}
