using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerWeapons : MonoBehaviour
{
    [Header("----- Components -----")]
    [SerializeField] AudioSource aud;
    [SerializeField] Transform attPos;

    [Header("----Weapon stats----")]
    [SerializeField] List<WeaponStats> weaponlist = new List<WeaponStats>();
    [SerializeField] GameObject weaponModel;
    [SerializeField] float shootRate;
    [SerializeField] int shootDamage;
    [SerializeField] int shootDist;

    //[Header("~~~~~ Weapon Power Ups ~~~~~")]
    //[SerializeField] private bool damageBoosted;
    //[SerializeField] private float damageBoostAmount = 1.2f;

    int selectedweapon = 0;
    bool isShooting;
    bool reload;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerSaveState.instance != null)
        {
            InitializeWeapons();
        }
        changeWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.isPaused)
        {
            selectGun();

            if (Input.GetButtonDown("Shoot") && !isShooting && !reload)
            {
                StartCoroutine(Shoot());
            }

            if (Input.GetButton("Reload") && !isShooting && !reload)
            {
                StartCoroutine(RELOADING());
            }
        }
    }

    public void InitializeWeapons()
    {
        if (PlayerSaveState.instance.Weapons.Count != 0)
        {
            weaponlist.Clear();

            foreach (WeaponStats savedWeapon in PlayerSaveState.instance.Weapons)
            {
                SetWeaponStats(savedWeapon);
            }
        }
    }

    IEnumerator Shoot()
    {
        // check if we have ammo in the currently selected gun
        if (weaponlist.Count > 0)
        {
            if (true)
            {
                isShooting = true;
                aud.PlayOneShot(weaponlist[selectedweapon].shootsound, weaponlist[selectedweapon].shootsoundvol);

                weaponlist[selectedweapon].Attack(attPos);
                
                //weaponlist[selectedweapon].ammmoCur--;
                //GameManager.instance.updateAmmoUI(weaponlist[selectedweapon].ammmoCur, weaponlist[selectedweapon].ammmoMax);
                // once fired pause 
                yield return new WaitForSeconds(shootRate);
                //stop shooting 
                isShooting = false;
            }
        }
    }

    IEnumerator RELOADING()
    {
        if (weaponlist.Count > 0)
        {
            reload = true;

            //weaponlist[selectedweapon].ammmoCur = weaponlist[selectedweapon].ammmoMax;
            //GameManager.instance.updateAmmoUI(weaponlist[selectedweapon].ammmoCur, weaponlist[selectedweapon].ammmoMax);
            yield return new WaitForSeconds(3F);
            reload = false;
        }
    }

    public void SetWeaponStats(WeaponStats weapon)
    {
        weaponlist.Add(weapon);
        //stats
        shootDamage = weapon.shootDamage;
        shootDist = weapon.shootDist;
        shootRate = weapon.shootRate;
        //model
        weaponModel.GetComponent<MeshFilter>().sharedMesh = weapon.model.GetComponent<MeshFilter>().sharedMesh;

        weaponModel.GetComponent<MeshRenderer>().sharedMaterial = weapon.model.GetComponent<MeshRenderer>().sharedMaterial;
        selectedweapon = weaponlist.Count - 1;

        //GameManager.instance.updateAmmoUI(weaponlist[selectedweapon].ammmoCur, weaponlist[selectedweapon].ammmoMax);
    }

    void selectGun()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && selectedweapon < weaponlist.Count - 1)
        {
            selectedweapon++;
            changeWeapon();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && selectedweapon > 0)
        {
            selectedweapon--;
            changeWeapon();
        }
    }

    void changeWeapon()
    {
        shootDamage = weaponlist[selectedweapon].shootDamage;
        shootDist = weaponlist[selectedweapon].shootDist;
        shootRate = weaponlist[selectedweapon].shootRate;
        //model
        weaponModel.GetComponent<MeshFilter>().sharedMesh = weaponlist[selectedweapon].model.GetComponent<MeshFilter>().sharedMesh;
        weaponModel.GetComponent<MeshRenderer>().sharedMaterial = weaponlist[selectedweapon].model.GetComponent<MeshRenderer>().sharedMaterial;

        //GameManager.instance.updateAmmoUI(weaponlist[selectedweapon].ammmoCur, weaponlist[selectedweapon].ammmoMax);

        isShooting = false;
    }
}
