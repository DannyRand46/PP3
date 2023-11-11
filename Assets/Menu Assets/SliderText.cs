using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderText : MonoBehaviour
{
    TextMeshProUGUI mText;
    // Start is called before the first frame update
    void Start()
    {
        mText = GetComponent<TextMeshProUGUI>();
    }

    public void SetSliderText(float sliderValue)
    {
        int v = (int)(sliderValue * 100);
        if (mText == null)
        {
            mText = GetComponent<TextMeshProUGUI>();
        }
        if (mText != null)
        {
            mText.text = v.ToString();
        }
    }
}
