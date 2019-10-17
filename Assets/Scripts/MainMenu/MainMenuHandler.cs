using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour {
    public void OpenTRexRunner()
    {
        SceneManager.LoadScene("TRexRunner");
    }
	public void OpenFlappyBird()
    {
        SceneManager.LoadScene("FlappyBird");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
