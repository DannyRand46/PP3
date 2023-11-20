using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PowerUpManager;

public class PlayerSaveState : MonoBehaviour
{
    public static PlayerSaveState instance = new PlayerSaveState();
    //Stats
    public float PlayerHealth;
    public float PlayerMana;

    //Inventory
    public List<WeaponStats> Weapons;
    public Dictionary<PowerUpType, bool> PowerUps;
    public int pots;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        Save();

        DontDestroyOnLoad(gameObject);
    }

    public void Save()
    {
        //Save player stats
        PlayerHealth = GameManager.instance.player.GetComponent<PlayerController>().Hp;
        PlayerMana = GameManager.instance.player.GetComponent<PlayerController>().Mana;

        GameObject cons = GameObject.FindWithTag("Consumables");
        if ( cons != null )
        {
            pots = cons.GetComponent<Consumeables>().pots;
        }

        //if (GameManager.instance.player.GetComponent<PowerUpManager>().acquiredPowerUps != null)
        //{
        //    foreach (KeyValuePair<PowerUpType, bool> power in GameManager.instance.player.GetComponent<PowerUpManager>().acquiredPowerUps)
        //    {
        //        if (power.Value)
        //        {
        //            PowerUps.Add(power.Key, power.Value);
        //        }
        //    }
        //}
    }
}
