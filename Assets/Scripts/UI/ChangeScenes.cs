using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public void GoToGame()
    {
        SceneManager.LoadScene("Game level 1");
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
    public void GoToEscogerNave()
    {
        SceneManager.LoadScene("EscogerNave");
    }
    public void GoToGameLevel2()
    {
        SceneManager.LoadScene("Game level 2");

    }
    public void TimeScaleGo()
    {
        Time.timeScale = 1f;
    }
    public void TimeScaleStop()
    {
        Time.timeScale = 0f;
    }
    public void Salir()
    {
        Application.Quit();
    }
    
}
