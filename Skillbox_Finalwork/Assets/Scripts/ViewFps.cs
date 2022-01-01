using UnityEngine;
using TMPro;

public class ViewFps : MonoBehaviour
{
    [SerializeField] private ColorFpsText[] _colorFpsText;

    [SerializeField] private TMP_Text _fpsText;
    [SerializeField] private CounterFps _counterFps;

    private string[] _from0To1000 = new string[2000];

    private void Awake()
    {
        for(int i = 0; i < _from0To1000.Length; i++)
        {
            _from0To1000[i] = i.ToString();
        }
    }

    private void Update()
    {
        _fpsText.text = GlobalStringsVars.FpsText + _from0To1000[_counterFps.Fps];
        SetColorFpsText(_fpsText);
    }

    private void SetColorFpsText(TMP_Text text)
    {
        for (int i = 0; i < _colorFpsText.Length; i++)
        {
            if(_counterFps.Fps < _colorFpsText[i]._minFps)
            {
                text.color = _colorFpsText[i]._color;
                break;
            }
        }
    }

    [System.Serializable]
    private struct ColorFpsText
    {
        public Color _color;
        public int _minFps;
    }
}