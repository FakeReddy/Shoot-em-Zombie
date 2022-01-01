using UnityEngine;

public class CallPanel : GameData
{
    [SerializeField] private GameObject _losePanel;

    public void Lose()
    {
        _losePanel.SetActive(true);
        SaveGameData();
        _currentRounds = 0;
        _currentScore = 0;
        Time.timeScale = 0;
    }
}