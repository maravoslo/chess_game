using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    public void StartGame()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Help()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    public void Exit(){
        Application.Quit();
    }
}
