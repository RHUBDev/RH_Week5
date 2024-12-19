using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public TMP_Text scorestext;
    public TMP_Text mousetext;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Mouse Sensitivity"))
        {
            slider.value = PlayerPrefs.GetFloat("Mouse Sensitivity");
        }
        else
        {
            slider.value = 400;
        }
        mousetext.text = "Mouse Sensitivity\n\n" + slider.value;
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
        scorestext.text = "HighScore:\nWave: " + highwave + ",\nKills: " + highkills + "\nCo-Op HighScore:\nWave: " + multihighwave + ",\nKills: " + multihighkills;
    }

    public void OnSliderChanged()
    {
        mousetext.text = "Mouse Sensitivity\n\n" + slider.value;
    }

    private void SetMouse()
    {
        PlayerPrefs.SetFloat("Mouse Sensitivity", slider.value);
    }

    public void Button1()
    {
        SetMouse();
        PlayerPrefs.SetString("LoadingLevel", "MyScene1");
        SceneManager.LoadScene("OmniScene");
    }

    public void Button2()
    {
        SetMouse();
        PlayerPrefs.SetString("LoadingLevel", "InfiniteScene");
        SceneManager.LoadScene("OmniScene");
    }

    public void Button3()
    {
        SetMouse();
        PlayerPrefs.SetString("LoadingLevel", "BattleMultiplayer");
        SceneManager.LoadScene("OmniScene");
    }

    public void Button4()
    {
        SetMouse();
        PlayerPrefs.SetString("LoadingLevel", "CompetitiveMultiplayer");
        SceneManager.LoadScene("OmniScene");
    }

    public void Button5()
    {
        SetMouse();
        PlayerPrefs.SetString("LoadingLevel", "CoOpMultiplayer");
        SceneManager.LoadScene("OmniScene");
    }

    public void Button6()
    {
        SetMouse();
        PlayerPrefs.SetString("LoadingLevel", "InfiniteMultiplayer");
        SceneManager.LoadScene("OmniScene");
    }
}
