using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float distance;
    private float time;
    private Vector2 direction;
    private string shooterTag;
    public AudioClip sound;
    [SerializeField]
    private GameObject deathUI;


    public void Setup(Vector2 direction, string tag)
    {
        this.direction = direction;
        transform.right = direction * -1;
        shooterTag = tag;
    }

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        deathUI = GameObject.FindGameObjectWithTag("DeathUI");
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= distance)
            Destroy(gameObject);
        transform.position -= (Vector3)direction.normalized * Time.deltaTime * speed;
        time += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == shooterTag || collision.tag == "Weapons" || collision.tag == "Detection" || collision.tag == "Bullet" || collision.tag == "PlayerSound")
            return;

        if (collision.tag == "Player")
        {
            deathUI.transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("AudioDeath").GetComponent<AudioSource>().Play();
            collision.gameObject.SetActive(false);

        }

        if (collision.tag == "Enemies")
        {
            GameObject.Find("AudioDeath").GetComponent<AudioSource>().Play();
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
        
    }
}
