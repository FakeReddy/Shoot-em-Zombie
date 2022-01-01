using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneView : GameData
{
    [SerializeField] private GameObject _pausePanel;
    public void Play()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void Resume()
    {
        Time.timeScale = 1;
        SaveGameData();
        _pausePanel.SetActive(false);
    }
    public void Pause()
    {
        Time.timeScale = 0;
        _pausePanel.SetActive(true);
    }
    public void Menu()
    {
        Time.timeScale = 1;
        SaveGameData();
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        SaveGameData();
        Application.Quit();
    }
}