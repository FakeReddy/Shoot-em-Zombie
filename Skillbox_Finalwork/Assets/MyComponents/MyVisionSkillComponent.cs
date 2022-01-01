using UnityEngine;

public class MyVisionSkillComponent : MonoBehaviour
{
    [SerializeField] private int _rangeVision;
    [SerializeField] private int _timeVision;

    [SerializeField] private GameObject _sphere;

    public GameObject Sphere => _sphere;

    public int RangeVision => _rangeVision;
    public int TimeVision => _timeVision;
}
