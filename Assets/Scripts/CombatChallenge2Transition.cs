using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatChallenge2Transition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerSaveState.instance.Save();
            SceneManager.LoadScene("CombatChallengeRoom2");
        }
    }
}
