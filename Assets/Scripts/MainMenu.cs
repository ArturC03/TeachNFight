using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void ExitGame()
    {
        Console.WriteLine("Exiting game");
        Application.Quit();
    }
    
    public void ReturnHome(){
        SceneManager.LoadSceneAsync(0);
    }
}