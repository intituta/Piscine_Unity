using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapons : MonoBehaviour
{
    [SerializeField]
    private WeaponCreation[] weaponChoice;

    public Transform tr;
    public SpriteRenderer weapon;
    public int ammoLeft;
    public int index;
    public WeaponCreation weaponChosen;

    void Start()
    {
        tr = GetComponent<Transform>();
        weapon = GetComponent<SpriteRenderer>();
        index = Random.Range(0, weaponChoice.Length);
        weaponChosen = weaponChoice[index];
        weapon.sprite = weaponChosen.artWorkUnequip;
        ammoLeft = weaponChosen.ammo;
    }
}
