using UnityEngine;

public class CounterFps : MonoBehaviour
{
    [SerializeField] private int _frameRange = 60;

    private int[] _fpsBuffer;
    private int _fpsBufferIndex;

    public int Fps { get; private set; }

    private void Update()
    {
        if (_fpsBuffer == null || _frameRange != _fpsBuffer.Length)
            InitializateBuffer();
        UpdateBuffer();
        CalculateFps();
    }

    private void InitializateBuffer()
    {
        if(_frameRange <= 0)
        {
            _frameRange = 1;
        }
        _fpsBuffer = new int[_frameRange];
        _fpsBufferIndex = 0;
    }

    private void UpdateBuffer()
    {
        int numberOne = 1;
        _fpsBuffer[_fpsBufferIndex++] = (int)(numberOne / Time.unscaledDeltaTime);
        if(_fpsBufferIndex >= _frameRange)
        {
            _fpsBufferIndex = 0;
        }
    }

    private void CalculateFps()
    {
        int sum = 0;
        for (int i = 0; i < _frameRange; i++)
        {
            sum += _fpsBuffer[i];
        }
        Fps = sum / _frameRange;
    }
}