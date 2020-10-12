using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void sceneSelect(string sceneName) {
        SceneManager.LoadScene(int.Parse(sceneName));
    }
    public void exit()
    {
        Application.Quit();
    }
}
