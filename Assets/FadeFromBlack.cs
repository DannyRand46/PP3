using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeFromBlack : MonoBehaviour
{
    [SerializeField] Image fade;

    float fadeRatio;
    // Start is called before the first frame update
    void Start()
    {
        fadeRatio = 1;
        if (fade == null)
        {
            fade = GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
            fadeRatio -= Time.deltaTime;
            fade.color = new Color32(1, 2, 3, (byte)(fadeRatio * 255));
            if (fadeRatio <= 0)
            {
            Destroy(gameObject);
            }

    }
}
