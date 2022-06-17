using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScript : MonoBehaviour
{
	public AudioSource panelSound;
	public Animator animator;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			panelSound.Play();
			animator.SetTrigger("Sonic Collide");
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player" && !UIManagerScript.instance.gameEnded)
			UIManagerScript.instance.endGame();
	}
}
