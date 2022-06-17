using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour
{
	public static UIManagerScript instance { get; private set; }

	public GameObject player;
	public AudioSource lvlMusic;
	public AudioSource endLvlMusic;
	public GameObject signCamera;

	public GameObject gameUI;
	public GameObject scoreUI;
	public Text ringsScoreUI;
	public Text timeScoreUI;
	public Text finalScoreUI;

	[HideInInspector] public int rings;
	[HideInInspector] public int minutes;
	[HideInInspector] public int seconds;

	[HideInInspector] public bool gameEnded = false;

	private int finalScore;

	public void endGame()
	{
		gameEnded = true;
		lvlMusic.Stop();
		endLvlMusic.Play();
		StartCoroutine(displayFinalScore());

		ringsScoreUI.text = "x" + rings.ToString();
		timeScoreUI.text = minutes.ToString() + ":" + (seconds < 10 ? "0" : "") + seconds.ToString();
		finalScore = 100 * rings + (20000 - 100 * (seconds + 60 * minutes));

		PlayerPrefs.SetInt("Rings", PlayerPrefs.GetInt("Rings") + rings);
		if (PlayerPrefs.GetInt("lvl1 Best") < finalScore)
			PlayerPrefs.SetInt("lvl1 Best", finalScore);
		PlayerPrefs.Save();

		//signCamera.SetActive(true);
		//player.SetActive(false);
		gameUI.SetActive(false);
		scoreUI.SetActive(true);
	}

	private void Awake()
    {
		instance = this;
    }

	private IEnumerator displayFinalScore()
	{
		yield return new WaitForSeconds(6);
		finalScoreUI.text = "Score: " + finalScore.ToString();
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene("TitleScreen");
	}
}
