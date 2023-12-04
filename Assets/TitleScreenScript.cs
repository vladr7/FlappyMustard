using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour
{
    public void loadFlappyScene()
    {
        SceneManager.LoadScene("Scenes/Flappy");
    }

    public void loadSuika()
    {
        SceneManager.LoadScene("Scenes/Suika");

    }

    public void quitGame()
    {
        Application.Quit();
    }
}
