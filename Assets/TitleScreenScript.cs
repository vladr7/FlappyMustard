using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenScript : MonoBehaviour
{
    public String userName;
    public TMP_InputField inputField; 

    public void Start()
    {
        userName = PlayerPrefs.GetString("USER_NAME", "");
        inputField.text = userName;
    }
    
    public void ReadInput()
    {
        userName = inputField.text; 
        Debug.Log("Username: " + userName);
        PlayerPrefs.SetString("USER_NAME", userName);
    }

    public void loadFlappyScene()
    {
        ReadInput();
        if(userName == "")
            return;
        SceneManager.LoadScene("Scenes/Flappy");
    }

    public void loadSuika()
    {
        ReadInput();
        if(userName == "")
            return;
        SceneManager.LoadScene("Scenes/Suika");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
