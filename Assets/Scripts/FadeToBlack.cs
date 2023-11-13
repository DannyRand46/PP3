using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField] Image fade;
    public bool f;
    float fadeRatio;
    // Start is called before the first frame update
    void Start()
    {
        f = false;
        fadeRatio = 0;
        if (fade == null)
        {
            fade = GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (f)
        {
            fadeRatio += Time.deltaTime;
            fade.color = new Color32(1, 2, 3, (byte)(fadeRatio * 255));
            if (fadeRatio >= 1)
            {
                f = false;
            } 
        }

    }

    public void fading()
    {
        f = true;
    }
}