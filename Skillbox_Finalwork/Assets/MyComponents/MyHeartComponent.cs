using UnityEngine;

public class MyHeartComponent : MonoBehaviour
{
    [SerializeField] private int _takeHeal;
    public int TakeHeal => _takeHeal;
}