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
        PlayerPrefs.SetString("LoadingLevel", "Menu");
        int highwave = 0;
        int highkills = 0;
        int multihighwave = 0;
        int multihighkills = 0;

        if (PlayerPrefs.HasKey("HighWave"))
        {
            highwave = PlayerPrefs.GetInt("HighWave");
            highkills = PlayerPrefs.GetInt("HighKills");
        }
        if (PlayerPrefs.HasKey("MultiHighWave"))
        {
            multihighwave = PlayerPrefs.GetInt("MultiHighWave");
            multihighkills = PlayerPrefs.GetInt("MultiHighKills");
        }
        scorestext.text = "HighScore:\nWave: " + highwave + ",\nKills: " + highkills + "\n2 Player HighScore:\nWave: " + multihighwave + ",\nKills: " + multihighkills;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerPrefs.SetString("LoadingLevel", "MyScene1");
            SceneManager.LoadScene("OmniScene");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerPrefs.SetString("LoadingLevel", "InfiniteScene");
            SceneManager.LoadScene("OmniScene");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerPrefs.SetString("LoadingLevel", "BattleMultiplayer");
            SceneManager.LoadScene("OmniScene");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerPrefs.SetString("LoadingLevel", "CompetitiveMultiplayer");
            SceneManager.LoadScene("OmniScene");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayerPrefs.SetString("LoadingLevel", "CoOpMultiplayer");
            SceneManager.LoadScene("OmniScene");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            PlayerPrefs.SetString("LoadingLevel", "InfiniteMultiplayer");
            SceneManager.LoadScene("OmniScene");
        }
    }

    public void LoadOmniScene()
    {
        SceneManager.LoadScene("OmniScene");
    }

    public void LoadMyScene1()
    {
       PlayerPrefs.SetString("LoadingLevel", "MyScene1");
    }

    public void LoadInfiniteScene()
    {
        PlayerPrefs.SetString("LoadingLevel", "InfiniteScene");
    }

    public void LoadBattleMultiplayer()
    {
        PlayerPrefs.SetString("LoadingLevel", "BattleMultiplayer");
    }

    public void LoadCompetitiveMultiplayer()
    {
        PlayerPrefs.SetString("LoadingLevel", "CompetitiveMultiplayer");
    }

    public void LoadCoOpMultiplayer()
    {
        PlayerPrefs.SetString("LoadingLevel", "CoOpMultiplayer");
    }
}
