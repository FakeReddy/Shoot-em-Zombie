using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private bool _isVision;

    private float _rangeVision;

    private Vector3 _offset;
    private Vector3 _normalOffset;

    private void Start()
    {
        _normalOffset = transform.position - _target.position;
        _offset = _normalOffset;
    }

    private void LateUpdate()
    {
        _offset.y = _normalOffset.y + _rangeVision;
        transform.position = _target.position + _offset;
    }

    public void ApplyVision(float range, float timeVision)
    {
        _rangeVision = range;
        StartCoroutine(SetNormalOffset(timeVision));
    }

    private IEnumerator SetNormalOffset(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _rangeVision = 0;
    }
}