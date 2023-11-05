using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUpDisplayManager : MonoBehaviour
{
    public TextMeshProUGUI powerUpText;

    public void UpdatePowerUpDisplay(string powerUpName)
    {
        powerUpText.text = "Active Power-Up: " + powerUpName;
    }
}
