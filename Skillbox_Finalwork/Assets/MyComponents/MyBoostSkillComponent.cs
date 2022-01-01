using UnityEngine;

public class MyBoostSkillComponent : MonoBehaviour
{
    [SerializeField] private int _takeBoost;
    [SerializeField] private int _timeBoost;

    [SerializeField] private GameObject _sphere;

    public GameObject Sphere => _sphere;

    public int TakeBoost => _takeBoost;
    public int TimeBoost => _timeBoost;
}