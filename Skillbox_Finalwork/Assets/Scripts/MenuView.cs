using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
internal class HightTexts
{
    [SerializeField] internal TextMeshProUGUI _hightScoreText;
    [SerializeField] internal TextMeshProUGUI _hightRoundsText;
}
[System.Serializable]
internal class MenuSliders
{
    [SerializeField] internal Slider _musicSlider;
    [SerializeField] internal Slider _soundSlider;
}
public class MenuView : GameData
{
    [SerializeField] private HightTexts _hightTexts;
    [SerializeField] private MenuSliders _menuSliders;

    private bool _isOneAwake = false;

    private void Start()
    {
        if (_hightTexts._hightScoreText != null)
        {
            SetText(ref _hightTexts._hightScoreText, GlobalStringsVars.HightScoreText + _hightScore.ToString());
        }
        if (_hightTexts._hightRoundsText != null)
        {
            SetText(ref _hightTexts._hightRoundsText, GlobalStringsVars.HightRoundsText + _hightRounds.ToString());
        }
        if (_menuSliders._musicSlider != null)
        {
            SetSliderValue(_menuSliders._musicSlider, LoadFloatData(GlobalStringsVars.MusicValueData));
        }
        if (_menuSliders._soundSlider != null)
        {
            SetSliderValue(_menuSliders._soundSlider, LoadFloatData(GlobalStringsVars.SoundValueData));
        }
    }
    public void SaveValueMusic()
    {
        SetToCurrentMusicValue(_menuSliders._musicSlider.value);
        if (_isOneAwake) // Делаю такую проверку на 1 раз потому что Юнити почему-то самого начала раз вызывает то что вызывает Слайдер
        {
            SaveMenuData();
        }
        else
        {
            _isOneAwake = true;
        }
    }
    public void SaveValueSound()
    {
        SetToCurrentSoundValue(_menuSliders._soundSlider.value);
        if (_isOneAwake) // Делаю такую проверку на 1 раз потому что Юнити почему-то самого начала раз вызывает то что вызывает Слайдер
        {
            SaveMenuData();
        }
        else
        {
            _isOneAwake = true;
        }
    }

    private void SetText(ref TextMeshProUGUI Text, string SetText)
    {
        Text.text = SetText;
    }

    private void SetSliderValue(Slider Slider, float Number)
    {
        Slider.value = Number;
    }
}