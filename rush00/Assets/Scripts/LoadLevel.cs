using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private int stairDir;
    [SerializeField]
    private string levelName;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");

        if (enemies.Length == 0 && coll.gameObject.tag == "Player" && stairDir == 1)
        {
            PlayerPrefs.SetInt(levelName, 1);
            Destroy(GameObject.Find("AudioBackground"));
            AudioBackground.instance = null;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + stairDir);
        }
        else if (coll.gameObject.tag == "Player" && stairDir == -1)
        {
            PlayerPrefs.SetInt(levelName, 0);
            Destroy(GameObject.Find("AudioBackground"));
            AudioBackground.instance = null;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + stairDir);
        }
    }

}
