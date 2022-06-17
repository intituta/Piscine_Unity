using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
	public Text timeUI;

	private int seconds;
	private int minutes;

	private void Start()
	{
		StartCoroutine(CountTime());
	}

	private IEnumerator CountTime()
	{
		while (!UIManagerScript.instance.gameEnded && minutes < 10)
		{
			yield return new WaitForSeconds(1);
			UIManagerScript.instance.seconds++;
			if (seconds++ == 60)
			{
				seconds = 0;
				UIManagerScript.instance.seconds = 0;
				minutes++;
				UIManagerScript.instance.minutes++;
			}
			timeUI.text = minutes.ToString() + ":" + (seconds < 10 ? "0" : "") + seconds.ToString();
		}
	}
}
