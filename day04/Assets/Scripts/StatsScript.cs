using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsScript : MonoBehaviour
{

	public Text totalRings;
	public Text totalLives;

	// Start is called before the first frame update
	void Start()
	{
		totalRings.text = PlayerPrefs.GetInt("Rings").ToString();
		totalLives.text = PlayerPrefs.GetInt("Lost Lives").ToString();
	}
}
