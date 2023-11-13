using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CubePuzzle : MonoBehaviour
{
    public GameObject[] cubes;
    public Vector3[] correctRotations;
    public GameObject door;
    //timer
    [SerializeField] float timer = 120f;
    private bool timerStarted = false;
    public TextMeshProUGUI timerText;


    void Start()
    {
        timerText.enabled = false;
    }
    void Update()
    {
        if(timerStarted)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();
            if(timer <= 0)
            {
                TimerEnded();
            }
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartTimer()
    {
        if(!timerStarted)
        {
            timerStarted = true;
            timerText.enabled = true;
        }
    }

    private void TimerEnded()
    {
        UnlockDoor();
        timerText.enabled = false;
    }

    public void CheckPuzzle()
    {
        for(int i = 0; i < cubes.Length; i++)
        {
            if (Quaternion.Euler(correctRotations[i]) != cubes[i].transform.rotation)
            {
                return;
            }
        }

        UnlockDoor();
        Currency.instance.GainDrachma(50);
    }

    void UnlockDoor()
    {
        Destroy(door);
        timerText.enabled = false;
    }
}
