using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

[CreateAssetMenu]
public class WeaponStats : ScriptableObject
{
    public float shootRate;
    public int shootDamage;
    public int shootDist;
    public int manaCost;

    public GameObject model;
    public ParticleSystem hiteffect;
    public AudioClip shootsound;
    [Range(0, 1)] public float shootsoundvol;

    public void Attack(Transform attPos)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDist))
        {
            //Does he object have acess to IDamage
            IDamage damagable = hit.collider.GetComponent<IDamage>();
            // if that object is damagebel then damge it  
            if (damagable != null)
            {
                damagable.TakeDamage(shootDamage);
            }
            Instantiate(hiteffect, hit.point, Quaternion.identity);
        }
    }
}
