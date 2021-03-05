using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    GameObject[] players;
    public GameObject panelDeath;
    public Text distance;
    public Text level;
    int counter = 0;

    float time = 0;
    void Start()
    {
        level.text = "level: " + PlayerPrefs.GetInt("level").ToString();
    }
    void FixedUpdate()
    {
        death();
        time += Time.deltaTime;
        if (time >= 1)
        {
            counter++;
            distance.text = "distances" + (10 * counter).ToString();
            time = 0;
        }

    }
    private void death()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 0)
        {
            Time.timeScale = 0;
            panelDeath.SetActive(true);
            PlayerPrefs.SetInt("score", 0);
        }
    }

    public void replay()
    {
        panelDeath.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
