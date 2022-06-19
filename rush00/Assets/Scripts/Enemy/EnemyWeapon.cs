using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField]
    private WeaponCreation[] weaponChoice;
    [SerializeField]
    private SpriteRenderer weaponSR;
    private WeaponCreation weaponChosen;
    private bool allowFire;

    public bool isDetect;
    public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        int i = Random.Range(0, weaponChoice.Length);
        weaponChosen = weaponChoice[i];
        weaponSR.sprite = weaponChosen.artWorkEquip;
        allowFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        // if (isDetect == true)
        // {
        //     transform.up = direction;
        //     if (allowFire == true)
        //         StartCoroutine(FireRate(direction));
        // }
    }

    public void ShootPlayer(Vector2 dir)
    {
        // transform.up = dir;
        direction = transform.up;
            if (allowFire == true)
                StartCoroutine(FireRate());
    }

    IEnumerator FireRate()
    {
        allowFire = false;
        GameObject tmp = Instantiate(weaponChosen.bulletObj, weaponSR.transform.position, Quaternion.identity);
        //GetComponent<AudioSource>().PlayOneShot(tmp.GetComponent<Bullet>().sound);
        tmp.GetComponent<Bullet>().Setup(direction, gameObject.tag);
        yield return new WaitForSeconds(weaponChosen.fireRate);
        allowFire = true;
    }
}
