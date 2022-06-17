using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserNameScript : MonoBehaviour
{
	public InputField nameField;

	private void Start()
	{
		nameField.text = PlayerPrefs.GetString("User Name");
	}

	public void UpdateUserName(string name)
	{
		PlayerPrefs.SetString("User Name", name);
		PlayerPrefs.Save();
	}
}
