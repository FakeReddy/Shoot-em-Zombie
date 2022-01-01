using UnityEngine;

public class MyZombieComponent : MonoBehaviour
{
    [SerializeField] private int _addScoreToKill;
    public int AddScoreToKill => _addScoreToKill;
}