using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]

public class PowerUpManager : MonoBehaviour
{
    public Dictionary<PowerUpType, bool> acquiredPowerUps = new Dictionary<PowerUpType, bool>();
    private PlayerController playerController;

    private List<PowerUpType> availablePowerUps = new List<PowerUpType>();
    private int currentPowerUpIndex = 0;
    [Range(1,100)][SerializeField] int manaCostSheild;
    [Range(1,100)][SerializeField] int manaCostDamageReduction;
    [Range(1,100)][SerializeField] int manaCostSpeedBoost;
    [Range(1,100)][SerializeField] int manaCostRaiseHp;
    [Range(1,100)][SerializeField] int manaCostInvisibility;

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
        InitializePowerUps();
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
                    {

                        if (playerController.ConsumeMana(manaCostSheild))
                        {
                            playerController.ShieldOn(); 
                        }
                    }
                    break;

                case PowerUpType.SpeedBoost:
                    if (playerController.speedBoosted)
                        playerController.SpeedBoostOff();
                    else
                    {
                        if (playerController.ConsumeMana(manaCostSpeedBoost))
                        {
                            playerController.SpeedBoostOn(); 
                        }
                    }
                    break;

                case PowerUpType.RaiseHP:
                    if (playerController.healthBoosted)
                        playerController.HealthBoostOff();
                    else
                    {
                        if (playerController.ConsumeMana(manaCostRaiseHp))
                        {
                            playerController.HealthBoostOn(); 
                        }
                    }
                    break;

                case PowerUpType.Invisibility:
                    if (!playerController.IsInvisible())
                    {
                        if (playerController.ConsumeMana(manaCostInvisibility))
                        {
                            playerController.InvisibilityActive(); 
                        }
                    }
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
            PlayerSaveState.Instance.PowerUps[type] = true;

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

    public void InitializePowerUps()
    {
        foreach (KeyValuePair<PowerUpType, bool> entry in PlayerSaveState.Instance.PowerUps) 
        {
            if (entry.Value) 
            {
                AcquiredPowerUp(entry.Key);
            }
        }
    }
}
