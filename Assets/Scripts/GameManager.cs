using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static PowerUpManager;
//using UnityEditor.ProBuilder;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("----- Player -----")]
    public GameObject player;
    public GameObject playerSpawn;
    public Image playerHPBar;
    public Image playerManaBar;
    public Image playerStaminaBar;
    public PlayerController playerScript;
    public GameObject devCheat;
    public GameObject lightningCheat;
    public GameObject fireCheat;
    public GameObject treasureCheat;

    [Header("----- Enemy -----")]
    public GameObject enemySpawn;
    [SerializeField] int rangedEnemiesMax;
    [SerializeField] int meleeEnemiesMax;
    [SerializeField] int phantomsMax;
    [SerializeField] GameObject HPPickup;
    public GameObject maze;

    [Header("----Menus----")]
    [SerializeField] GameObject activeMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject lossMenu;
    [SerializeField] GameObject menuActiveInterface;
    [SerializeField] TextMeshProUGUI ammoCur;
    [SerializeField] TextMeshProUGUI ammoMax;
    public GameObject consumables;
    [SerializeField] GameObject PlayerDamageFlashScreen;
    //[SerializeField] GameObject PowerUpDisplay;

    public bool isPaused;
    float timeScaleOrig;

    //Only uncomment code once implemented
    void Awake()
    {
        instance = this;
        timeScaleOrig = Time.timeScale;
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerController>();
    }

    private void Start()
    {
        //InitializePlayerState();
        playerSpawn = GameObject.FindWithTag("Respawn");
    }


    void Update()
    {
        SetCheats();

        if (Input.GetButtonDown("Cancel") && activeMenu == null)
        {
            pausedState();
            activeMenu = pauseMenu;
            activeMenu.SetActive(isPaused);
        }
    }

    private void InitializePlayerState()
    {
        PlayerWeapons playerWeapons = player.GetComponent<PlayerWeapons>();

        if (PlayerSaveState.instance != null)
        {
            PowerUpManager powerUpManager = player.GetComponent<PowerUpManager>();
            if (playerScript != null)
            {
                playerScript.Hp = PlayerSaveState.instance.PlayerHealth;
                playerScript.Mana = PlayerSaveState.instance.PlayerMana;

                foreach (WeaponStats weapon in PlayerSaveState.instance.Weapons) 
                {
                    playerWeapons.SetWeaponStats(weapon);
                }

                powerUpManager.InitializePowerUps();
            }
        }
    }

    // Pauses the game
    public void pausedState()
    {
        isPaused = !isPaused;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        menuActiveInterface.gameObject.SetActive(false);
    }

    //Resumes the game
    public void unpausedState()
    {
        isPaused = !isPaused;
        Time.timeScale = timeScaleOrig;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if(activeMenu != null)
        {
            activeMenu.SetActive(false);
            activeMenu = null;
        }
        menuActiveInterface.gameObject.SetActive(true);
    }

    public void youLose()
    {
        lossMenu.SetActive(true);
        activeMenu = lossMenu;
        pausedState();
    }

    public void youWin()
    {
        winMenu.SetActive(true);
        activeMenu = winMenu;
        pausedState();
    }

    public IEnumerator flash()
    {
        PlayerDamageFlashScreen.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        PlayerDamageFlashScreen.SetActive(false);
    }

    public void updateAmmoUI(int cur, int max)
    {
        ammoCur.text = cur.ToString("F0");
        ammoMax.text = max.ToString("F0");
    }

    public int GetMaxRanged()
    {
        return rangedEnemiesMax;
    }

    public int GetMaxMelee()
    {
        return meleeEnemiesMax;
    }

    public int GetMaxPhantom()
    {
        return phantomsMax;
    }

    public void HealthDrop(Transform pos)
    {
        Instantiate(HPPickup, pos.position, Quaternion.identity);
    }

    void SetCheats()
    {
        if (devCheat == null)
        {
            devCheat = GameObject.FindWithTag("Dev Only");
        }
        if(lightningCheat == null)
        {
            lightningCheat = GameObject.FindWithTag("Lightning Staff");
        }
        if(fireCheat == null)
        {
            fireCheat = GameObject.FindWithTag("Fire Staff");
        }
        if(treasureCheat == null)
        {
            treasureCheat = GameObject.FindWithTag("Treasure");
        }
    }
}
