using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
internal class UiViewComponents
{
    [SerializeField] internal Spawner _spawner;
    [SerializeField] internal Weapon _weapon;
    [SerializeField] internal Health _health;
}

[System.Serializable]
internal class HealthUi
{
    [SerializeField] internal Image _healthBarPanel;
    [SerializeField] internal TextMeshProUGUI _healthBarText;
}

[System.Serializable]
internal class RoundsUi
{
    [SerializeField] internal TextMeshProUGUI _roundsText;
}

[System.Serializable]
internal class ScoresUi
{
    [SerializeField] internal TextMeshProUGUI _scoresText;
}

[System.Serializable]
internal class BulletsUi
{
    [SerializeField] internal Image _bulletsBarPanel;
    [SerializeField] internal TextMeshProUGUI _bulletsBarText;
}
public class UiView : GameData
{
    [SerializeField] private HealthUi _healthUi;
    [SerializeField] private RoundsUi _roundsUi;
    [SerializeField] private ScoresUi _scoresUi;
    [SerializeField] private BulletsUi _bulletsUi;

    [SerializeField] private UiViewComponents _components;

    private void Start()
    {
        Rounds();
        Scores();
        Bullets();
    }

    public void Health()
    {
        _healthUi._healthBarText.text = GlobalStringsVars.HealthText + _components._health.CurrentHealth + " / " + _components._health.MaxHealth;
        _healthUi._healthBarPanel.fillAmount = (float)_components._health.CurrentHealth / (float)_components._health.MaxHealth;
    }

    public void Rounds()
    {
        _roundsUi._roundsText.text = GlobalStringsVars.RoundsText + _currentRounds;
    }

    public void Scores()
    {
        _scoresUi._scoresText.text = GlobalStringsVars.ScoresText + _currentScore;
    }

    public void Bullets()
    {
        _bulletsUi._bulletsBarText.text = GlobalStringsVars.BulletsText + _components._weapon.GetCurrentBullets() + " / " + _components._weapon.GetMaxBullets();
        _bulletsUi._bulletsBarPanel.fillAmount = (float)_components._weapon.GetCurrentBullets() / (float)_components._weapon.GetMaxBullets();
    }
}