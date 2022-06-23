using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class tank : MonoBehaviour {

	public int life = 100;
	public int mitrailletteDamage = 10;
	public int missileDamage = 30;
	public Text lifeText;
	public AudioSource explosion;
	public bool isDead = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void getHit(int damage)
	{
		if (this.life - damage <= 0)
		{
			StartCoroutine(this.explodeSound());
			return;
		}
		this.life -= damage;
		this.lifeText.text = this.life + " ❤";
		if (gameObject.tag == "Player")
			Debug.Log("Player life: " + this.life);
		else
		{
			Debug.Log("Enemy life: " + this.life);
		}
	}
	 
	IEnumerator explodeSound()
	{
		this.isDead = true;
		this.lifeText.text = "0 ❤";
		this.explosion.Play();
		yield return new WaitForSeconds(this.explosion.clip.length);
		if (gameObject.tag == "Player")
		{
			Debug.Log("Player dead !\nGame over !");
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		Destroy(gameObject);
		Debug.Log("Enemy destroyed !");
	}

}
