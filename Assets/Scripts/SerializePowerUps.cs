using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PowerUpManager;
[System.Serializable]

public class SerializePowerUps
{
    public List<PowerUpEntry> list;

    public SerializePowerUps(Dictionary<PowerUpType, bool> powerUps)
    {
        list = new List<PowerUpEntry>();

        foreach (var entry in powerUps) 
        {
            list.Add(new PowerUpEntry {type = entry.Key, isAcquired = entry.Value});
        }
    }

    public Dictionary<PowerUpType, bool> ToDictionary()
    {
        Dictionary<PowerUpType, bool> dictionary = new Dictionary<PowerUpType, bool>();

        foreach (PowerUpEntry entry in list) 
        {
            dictionary[entry.type] = entry.isAcquired;
        }
        return dictionary;
    }
}
