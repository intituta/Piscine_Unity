using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject[] buttons;
    [SerializeField]
    private float fadingSpeed = 0.01f;
    private int selection;
    private int neg;

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    private void Selected(GameObject obj)
    {
        var tmp = obj.GetComponent<Text>().color;
        if (tmp.a <= 0.2)
            neg = 1;
        if (tmp.a >= 1)
            neg = -1;
        tmp.a = obj.GetComponent<Text>().color.a + (fadingSpeed * neg);
        obj.GetComponent<Text>().fontStyle = FontStyle.Bold;
        obj.GetComponent<Text>().color = tmp;
    }

    private void ResetVal(GameObject obj)
    {
        obj.GetComponent<Text>().fontStyle = FontStyle.Normal;
        var tmp = obj.GetComponent<Text>().color;
        tmp.a = 1;
        obj.GetComponent<Text>().color = tmp;
        neg = -1;
    }

    // Start is called before the first frame update
    void Start()
    {
        selection = 0;
        PlayerPrefs.SetInt("Level1", 0);
    }

    // Update is called once per frame
    void Update()
    {
        Selected(buttons[selection]);
        if (Input.GetKeyDown("return"))
        {
            if (selection == 0)
                StartGame();
            if (selection == 1)
                ExitGame();
        }
        if (Input.GetKeyDown("down"))
        {
            ResetVal(buttons[selection]);
            selection--;
        }
        if (Input.GetKeyDown("up"))
        {
            ResetVal(buttons[selection]);
            selection++;
        }
        if (selection < 0)
            selection = 1;
        if (selection > 1)
            selection = 0;
    }
}
