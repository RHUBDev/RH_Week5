using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public TMP_Text scorestext;

    // Start is called before the first frame update
    void Start()
    {
        int highwave = 0;
        int highkills = 0;
        if (PlayerPrefs.HasKey("HighWave"))
        {
            highwave = PlayerPrefs.GetInt("HighWave");
            highkills = PlayerPrefs.GetInt("HighKills");
        }
        scorestext.text = "HighScore:\nWave: " + highwave + ",\nKills: " + highkills;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadMyScene1();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadInfiniteScene();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadBattleMultiplayer();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            LoadCompetitiveMultiplayer();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            LoadCoOpMultiplayer();
        }
    }

    public void LoadMyScene1()
    {
        SceneManager.LoadScene("MyScene1");
    }

    public void LoadInfiniteScene()
    {
        SceneManager.LoadScene("InfiniteScene");
    }

    public void LoadBattleMultiplayer()
    {
        SceneManager.LoadScene("BattleMultiplayer");
    }

    public void LoadCompetitiveMultiplayer()
    {
        SceneManager.LoadScene("CompetitiveMultiplayer");
    }

    public void LoadCoOpMultiplayer()
    {
        SceneManager.LoadScene("CoOpMultiplayer");
    }
}
