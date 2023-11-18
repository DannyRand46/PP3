using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WaterControl : MonoBehaviour
{
    [SerializeField] float riseSpeed = 0.1f;
    [SerializeField] float maxHeight = 10.0f;
    public ChallengeManager challengeManager;
    private AudioSource audioSource;
   
    

    public bool isRising = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        if (isRising)
        {
            if (transform.localScale.y < maxHeight)
            {
                transform.localScale += new Vector3(0, riseSpeed * Time.deltaTime, 0);
                transform.position += new Vector3(0, riseSpeed * Time.deltaTime / 2, 0);
                challengeManager.CheckChallengeStatus();
            }
            else
            {
                
                isRising = false;
                audioSource.Stop();
            }
        }
    }

    public void StartRising()
    {
        isRising = true;
        audioSource.Play();
        
    }
}
