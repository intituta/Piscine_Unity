using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsControl : MonoBehaviour
{
    [SerializeField] private SpriteRenderer weapon;

    [SerializeField]
    private Text weaponName;
    [SerializeField]
    private Text ammo;
    [SerializeField]
    private GameObject Sound;
    [SerializeField]
    private AudioClip emptyAmmo;

    private bool isEquip;
    private Weapons weaponsScript;
    private bool allowFire;
    private float time;
    
    // Start is called before the first frame update
    void Start()
    {
        isEquip = false;
        allowFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && isEquip == false)
        {
            Collider2D[] obj = Physics2D.OverlapPointAll(transform.position);
            foreach (Collider2D collider in obj)
            {
                if (collider.tag == "Weapons")
                {
                    weaponsScript = collider.GetComponent<Weapons>();
                    weapon.sprite = weaponsScript.weaponChosen.artWorkEquip;
                    weaponsScript.weapon.sprite = null;
                    isEquip = true;
                    weaponName.text = weaponsScript.weaponChosen.weaponName;
                    if (weaponsScript.ammoLeft == -1)
                        ammo.text = "inf";
                    else
                        ammo.text = weaponsScript.ammoLeft.ToString();
                    break;
                }
            }
        }
        if (Input.GetMouseButtonDown(1) && isEquip == true)
        {
            weapon.sprite = null;
            StartCoroutine(UnequipWeapon(GetComponent<PlayerMovement>().direction));
            isEquip = false;
            weaponName.text = "None";
            ammo.text = "0";
        }
        if (Input.GetMouseButton(0) && isEquip == true && allowFire == true)
        {
            if (weaponsScript.ammoLeft == 0)
            {
                if (time >= 0.5f)
                {
                    GetComponent<AudioSource>().PlayOneShot(emptyAmmo);
                    time = 0;
                }
            }
            else
            {
                Sound.SetActive(true);
                Sound.GetComponent<CircleCollider2D>().radius = weaponsScript.weaponChosen.soundRadius;
                StartCoroutine(FireRate());
                StartCoroutine(StopSound());
            }
        }
        time += Time.deltaTime;
    }

    IEnumerator StopSound()
    {
        yield return new WaitForSeconds(0.5f);
        Sound.SetActive(false);
    }

    IEnumerator FireRate()
    {
        allowFire = false;
        GameObject tmp = Instantiate(weaponsScript.weaponChosen.bulletObj, weapon.transform.position, Quaternion.identity);
        GetComponent<AudioSource>().PlayOneShot(tmp.GetComponent<Bullet>().sound);
        tmp.GetComponent<Bullet>().Setup(GetComponent<PlayerMovement>().direction, gameObject.tag);
        if (weaponsScript.ammoLeft > 0)
        {
            weaponsScript.ammoLeft--;
            ammo.text = weaponsScript.ammoLeft.ToString();
        }
        yield return new WaitForSeconds(weaponsScript.weaponChosen.fireRate);
        allowFire = true;
    }

    IEnumerator UnequipWeapon(Vector2 direction)
    {
        weaponsScript.weapon.sprite = weaponsScript.weaponChosen.artWorkUnequip;
        weaponsScript.tr.position = transform.position;
        int range = Random.Range(10, 15);
        for (int i = 0; i < range; i++)
        {
            weaponsScript.tr.position -= (Vector3)direction.normalized * Time.deltaTime * 7f;
            weaponsScript.tr.eulerAngles += new Vector3(0, 0, 15);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
}
