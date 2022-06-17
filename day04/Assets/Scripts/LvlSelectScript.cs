using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LvlSelectScript : MonoBehaviour
{
	public Image selectIcon;
	public Text textField;
	public string lvlName;

	private bool unlocked;

	private void Start()
	{
		unlocked = (lvlName == "lvl1" || PlayerPrefs.GetInt(lvlName) == 1);
		if (!unlocked)
			gameObject.GetComponent<Image>().color = new Color(.5F, .5F, .5F, .5F);
		else
			textField.text = PlayerPrefs.GetInt(lvlName + " Best").ToString();
	}

	public void select()
	{
		if (unlocked)
			selectIcon.color = new Color(1, 1, 1, 1);
	}

	public void unselect()
	{
		if (unlocked)
			selectIcon.color = new Color(1, 1, 1, 0);
	}

	public void startLvl()
	{
		if (unlocked)
			SceneManager.LoadScene(lvlName);
	}
}
