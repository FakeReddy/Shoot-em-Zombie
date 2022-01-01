using UnityEngine;

public class MyBoxBulletComponent : MonoBehaviour
{
    [SerializeField] private int _takeBullets;
    public int TakeBullet => _takeBullets;
}