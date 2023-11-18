using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class SerializeWeapons
{
    public List<WeaponStats> list;

    public SerializeWeapons(List<WeaponStats> weapons) 
    {
        list = weapons;
    }

    public List<WeaponStats> ToList()
    {
        return list ?? new List<WeaponStats>();
    }
}
