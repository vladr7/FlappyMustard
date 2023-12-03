using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour
{
    public void loadMainScene()
    {
        Debug.Log("Load main scene");
        SceneManager.LoadScene("SampleScene");
        
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
