using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerUpManager : MonoBehaviour
{
    private Dictionary<PowerUpType, bool> acquiredPowerUps = new Dictionary<PowerUpType, bool>();
    private PlayerController playerController;

    private List<PowerUpType> availablePowerUps = new List<PowerUpType>();
    private int currentPowerUpIndex = 0;

    private PowerUpDisplayManager powerUpDisplayManager;

    public enum PowerUpType
    {
        DamageReduction,
        Shield,
        SpeedBoost,
        RaiseHP,
        Invisibility
    }

    void Start()
    {
        playerController = GetComponent<PlayerController>();

        foreach (PowerUpType type in Enum.GetValues(typeof(PowerUpType)))
        {
            acquiredPowerUps[type] = false;
        }

        powerUpDisplayManager = FindObjectOfType<PowerUpDisplayManager>();

        powerUpDisplayManager.UpdatePowerUpDisplay("None");
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CycleToNextPowerUp();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            ActivateSelectedPowerUp();
        }
    }

    public void AcquiredPowerUp(PowerUpType type)
    {
        if (!acquiredPowerUps[type])
        {
            acquiredPowerUps[type] = true;
        }
    }

    public void TogglePowerUp(PowerUpType type)
    {
        if (acquiredPowerUps[type])
        {
            switch (type)
            {
                case PowerUpType.Shield:
                    if (playerController.shielded)
                        playerController.ShieldOff();
                    else
                        playerController.ShieldOn();
                    break;

                case PowerUpType.SpeedBoost:
                    if (playerController.speedBoosted)
                        playerController.SpeedBoostOff();
                    else
                        playerController.SpeedBoostOn();
                    break;

                case PowerUpType.RaiseHP:
                    if (playerController.healthBoosted)
                        playerController.HealthBoostOff();
                    else
                        playerController.HealthBoostOn();
                    break;

                case PowerUpType.Invisibility:
                    if (!playerController.IsInvisible())
                        playerController.InvisibilityActive();
                    else
                        playerController.InvisibilityOff();
                    break;
            }
        }
    }

    private void CycleToNextPowerUp()
    {
        if (availablePowerUps.Count == 0)
        {
            Debug.Log("No power ups available");
            return;
        }

        currentPowerUpIndex = (currentPowerUpIndex + 1) % availablePowerUps.Count;
        PowerUpType currentPowerUp = availablePowerUps[currentPowerUpIndex];
    }

    private void ActivateSelectedPowerUp()
    {
        if (availablePowerUps.Count > 0)
        {
            PowerUpType currentPowerUp = availablePowerUps[currentPowerUpIndex];
            TogglePowerUp(currentPowerUp);
        }
    }

    public void AcquirePowerUp(PowerUpType type)
    {
        if (!acquiredPowerUps[type])
        {
            acquiredPowerUps[type] = true;

            if (!availablePowerUps.Contains(type))
            {
                availablePowerUps.Add(type);
            }

            UpdatePowerUpUI();
        }
    }

    private void UpdatePowerUpUI()
    {
        if (availablePowerUps.Count > 0)
        {
            PowerUpType currentPowerUp = availablePowerUps[currentPowerUpIndex];
            powerUpDisplayManager.UpdatePowerUpDisplay(currentPowerUp.ToString());
        }
        else
        {
            powerUpDisplayManager.UpdatePowerUpDisplay("None");
        }
    }
}
