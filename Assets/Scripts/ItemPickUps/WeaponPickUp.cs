using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField] WeaponStats weapon;
    [SerializeField] GameObject pickupText;

    bool playerInTrigger;
    void Start()
    {
        playerInTrigger = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInTrigger)
        {
            PickUpObject();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            pickupText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            pickupText.SetActive(true);
        }
    }

    public void PickUpObject()
    {
        //transfer gunstates to player
        GameManager.instance.player.GetComponent<PlayerWeapons>().SetWeaponStats(weapon);

        PlayerSaveState.instance.Weapons.Add(weapon);

        //Set weapon to no longer respawn when scene is reloaded
        if (MazeState.instance != null)
        {
            for (int i = 0; i < MazeState.instance.items1.Count; i++)
            {
                if (MazeState.instance.items1[i].item.CompareTag(gameObject.tag))
                {
                    MazeState.instance.DisableItem(i);
                    break;
                }
            }
        }

        Destroy(gameObject);
    }
}

