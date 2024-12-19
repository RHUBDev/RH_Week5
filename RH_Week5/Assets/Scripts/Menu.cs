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
        //unlock cursor on Menu
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (PlayerPrefs.HasKey("Mouse Sensitivity"))
        {
            //Get/Set mouse sensitivity slider value
            slider.value = PlayerPrefs.GetFloat("Mouse Sensitivity");
        }
        else
        {
            slider.value = 300;
        }
        mousetext.text = "Mouse\n\nSensitivity\n\n" + slider.value;
        PlayerPrefs.SetString("LoadingLevel", "Menu");
        int highwave = 0;
        int highkills = 0;
        int multihighwave = 0;
        int multihighkills = 0;

        //show previous high scores
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
        //Googled how to do rich text colours again
        scorestext.text = "<color=blue>HighScore:\nWave: " + highwave + ",\nKills: " + highkills + "\n</color><color=green>Co-Op HighScore:\nWave: " + multihighwave + ",\nKills: " + multihighkills +"</color>";
    }

    public void OnSliderChanged()
    {
        //set mouse sensitivity text after slider has been changed
        mousetext.text = "Mouse\n\nSensitivity\n\n" + slider.value;
    }

    private void SetMouse()
    {
        //save mouse sensitivity value
        PlayerPrefs.SetFloat("Mouse Sensitivity", slider.value);
    }

    public void Button1()
    {
        //save the name of the level type we are loading, so we can load the right settings in the game itself
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
