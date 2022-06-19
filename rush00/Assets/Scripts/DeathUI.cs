using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[] buttons;
    [SerializeField]
    private float fadingSpeed = 0.01f;
    private int selection;
    private int neg;

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
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
        neg = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Selected(buttons[selection]);
        if (Input.GetKeyDown("return"))
        {
            if (selection == 0)
                RestartLevel();
            if (selection == 1)
                ExitToMenu();
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
