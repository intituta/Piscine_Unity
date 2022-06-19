using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponCreation : ScriptableObject
{
    public Sprite artWorkUnequip;
    public Sprite artWorkEquip;
    public GameObject bulletObj;
    public string weaponName;
    public int ammo;
    public float fireRate;
    public float soundRadius;
}
