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
            if (EnemiesDefeated())
            {
                Currency.instance.GainDrachma(50);
                PlayerSaveState.instance.Save();
                SceneManager.LoadScene("ProtoType2");
            }
        }
    }

    private bool EnemiesDefeated()
    {
        bool noEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
        bool stoneGolemDefeated = GameObject.FindGameObjectsWithTag("StoneGolem").Length == 0;
        bool necroMancerDefeated = GameObject.FindGameObjectsWithTag("Necromancer").Length == 0;

        return noEnemy && stoneGolemDefeated && necroMancerDefeated;
    }
}
