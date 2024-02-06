using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        Screen.orientation= ScreenOrientation.LandscapeLeft;
    }
    public void StartButton()
    {
        SceneManager.LoadScene("01_Level01");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
