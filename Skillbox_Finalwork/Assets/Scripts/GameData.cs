using UnityEngine;
public class GameData : MonoBehaviour
{
    static protected int _currentScore = 0;
    static protected int _currentRounds = 0;

    protected int _hightScore;
    protected int _hightRounds;

    private float _currentMusicValue;
    private float _currentSoundValue;

    private void Awake()
    {
        _currentScore = 0;
        _currentRounds = 0;
        _hightScore = LoadIntData(GlobalStringsVars.HightScoreData);
        _hightRounds = LoadIntData(GlobalStringsVars.HightRoundsData);
    }

    public void AddToCurrentScore(int score)
    {
        AddIntNumber(ref _currentScore, score);
    }
    public void AddToCurrentRounds(int rounds)
    {
        AddIntNumber(ref _currentRounds, rounds);
    }

    public void SetToCurrentMusicValue(float value)
    {
        SetFloatNumber(ref _currentMusicValue, value);
    }
    public void SetToCurrentSoundValue(float value)
    {
        SetFloatNumber(ref _currentSoundValue, value);
    }

    public void SaveMenuData()
    {
        SaveFloat(GlobalStringsVars.MusicValueData, _currentMusicValue);
        SaveFloat(GlobalStringsVars.SoundValueData, _currentSoundValue);
    }
    public void SaveGameData()
    {
        if (_currentScore > _hightScore)
        {
            SaveInt(GlobalStringsVars.HightScoreData, _currentScore);
        }
        if (_currentRounds > _hightRounds)
        {
            SaveInt(GlobalStringsVars.HightRoundsData, _currentRounds);
        }
    }

    public int LoadIntData(string loadInt)
    {
        return PlayerPrefs.GetInt(loadInt);
    }
    public float LoadFloatData(string loadFloat)
    {
        return PlayerPrefs.GetFloat(loadFloat);
    }

    private void AddIntNumber(ref int number, int setNumber)
    {
        if (setNumber < 0)
        {
            Debug.LogError("GameData - apply below 0 score (AddIntNumber)");
        }
        else
        {
            number += setNumber;
            SaveGameData();
        }
    }
    private void SetFloatNumber(ref float number, float setNumber)
    {
        if (setNumber < 0)
        {
            Debug.LogError("GameData - apply below 0 score (SetFloatNumber)");
        }
        else
        {
            number = setNumber;
        }
    }

    private void SaveInt(string name, int number)
    {
        if (number < 0)
        {
            Debug.LogError("GameData - apply below 0 score (SetFloatNumber)");
        }
        else
        {
            PlayerPrefs.SetInt(name, number);
        }
    }
    private void SaveFloat(string name, float number)
    {
        if (number < 0)
        {
            Debug.LogError("GameData - apply below 0 score (SaveFloat)");
        }
        else
        {
            PlayerPrefs.SetFloat(name, number);
        }
    }
}