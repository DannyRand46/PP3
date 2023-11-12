using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PowerUpManager;

public class PlayerSaveState
{
    public static PlayerSaveState instance = new PlayerSaveState();
    //Stats
    public float PlayerHealth; //{ get; set; }
    public float PlayerMana; //{ get; set; }

    //public float PlayerStamina; //{ get; set; }
    //Inventory
    public List<WeaponStats> Weapons;//{ get; private set; }
    public Dictionary<PowerUpType, bool> PowerUps;
    
    public PlayerSaveState()
    {
        Weapons = new List<WeaponStats>();
        PowerUps = new Dictionary<PowerUpType, bool>();
        PlayerHealth = 20;
        PlayerMana = 500;
        //PlayerStamina = 100;
    }

    //public static PlayerSaveState Instance
    //{
    //    get { return instance; }
    //}

    public static PlayerSaveState Instance => instance;

    public void Save()
    {
        PlayerPrefs.SetFloat("PlayerHealth", PlayerHealth);
        PlayerPrefs.SetFloat("PlayerMana", PlayerMana);

        // Save weapon data
        SerializeWeapons serializableWeapons = new SerializeWeapons(Weapons);
        string weaponsJson = JsonUtility.ToJson(serializableWeapons);
        PlayerPrefs.SetString("PlayerWeapons", weaponsJson);

        // Save power-ups
        SerializePowerUps serializablePowerUps = new SerializePowerUps(PowerUps);
        string powerUpsJson = JsonUtility.ToJson(serializablePowerUps);
        PlayerPrefs.SetString("PlayerPowerUps", powerUpsJson);

        PlayerPrefs.Save();
    }

    public void Load()
    {
        // Load player stats
        PlayerHealth = PlayerPrefs.GetFloat("PlayerHealth", PlayerHealth); // Default to current value if not set
        PlayerMana = PlayerPrefs.GetFloat("PlayerMana", PlayerMana); // Default to current value if not set

        // Load weapon data
        string weaponsJson = PlayerPrefs.GetString("PlayerWeapons", "");
        if (!string.IsNullOrEmpty(weaponsJson))
        {
            SerializeWeapons serializableWeapons = JsonUtility.FromJson<SerializeWeapons>(weaponsJson);
            Weapons = serializableWeapons.ToList(); // Make sure you have a ToList method in SerializableWeapons class
        }

        // Load power-ups
        string powerUpsJson = PlayerPrefs.GetString("PlayerPowerUps", "");
        if (!string.IsNullOrEmpty(powerUpsJson))
        {
            SerializePowerUps serializablePowerUps = JsonUtility.FromJson<SerializePowerUps>(powerUpsJson);
            PowerUps = serializablePowerUps.ToDictionary(); // Make sure you have a ToDictionary method in SerializablePowerUps class
        }
    }

}
