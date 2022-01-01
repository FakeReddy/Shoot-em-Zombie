using UnityEngine;
using System;

[System.Serializable]
internal class HealthComponents
{
    [SerializeField] internal ActorView _actorView;
    [SerializeField] internal UiView _uiView;

    [SerializeField] internal ZombieAI _zombieAI;
    [SerializeField] internal PlayerMovement _playerMovement;

    [SerializeField] internal Collider _collider;
}

public class Health : MonoBehaviour
{
    [SerializeField] HealthComponents _components;

    [SerializeField] private int _currentHealth = 4;
    private bool _isAlive = true;
    private int _maxHealth;

    public bool IsAlive => _isAlive;
    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;

    private void Awake()
    {
        if (_currentHealth <= 0)
        {
            throw new ArgumentOutOfRangeException("_currentHealth in Health <= 0");
        }
        _maxHealth = _currentHealth;
        if (_components._uiView != null)
            _components._uiView.Health();
    }
    public void ApplyDamage(int damage)
    {
        if (damage < 0)
        {
            throw new ArgumentOutOfRangeException("damage in Health < 0");
        }
        else if (_currentHealth <= 0)
        {
            SetDeath();
            if (_components._uiView != null)
                _components._uiView.Health();
        }
        else if (_currentHealth <= damage)
        {
            SetDeath();
            if (_components._uiView != null)
                _components._uiView.Health();
        }
        else
        {
            SetHurtOnAnimation();
            _currentHealth -= damage;
            if (_components._uiView != null)
                _components._uiView.Health();
        }
        if (_isAlive == false)
        {
            SetDeath();
            Dead();
        }
    }
    public void ApplyHeal(int heal)
    {
        if (heal < 0)
        {
            throw new ArgumentOutOfRangeException("heal in Health < 0");
        }
        else if (_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
            if (_components._uiView != null)
                _components._uiView.Health();
        }
        else if (_currentHealth >= _maxHealth - heal)
        {
            _currentHealth = _maxHealth;
            if (_components._uiView != null)
                _components._uiView.Health();
        }
        else if (heal >= _maxHealth)
        {
            _currentHealth = _maxHealth;
            if (_components._uiView != null)
                _components._uiView.Health();
        }
        else
        {
            _currentHealth += heal;
            if (_components._uiView != null)
                _components._uiView.Health();
        }
        if (_isAlive == false)
        {
            SetDeath();
        }
    }

    private void SetDeath()
    {
        _currentHealth = 0;
        _isAlive = false;
    }

    private void SetStunOnAnimation()
    {
        if (_components._zombieAI != null)
        {
            _components._zombieAI.FollowTarget = false;
        }
        else if (_components._playerMovement != null)
        {
            _components._playerMovement.IsControlCharacter = false;
        }
    }
    private void SetHurtOnAnimation()
    {
        if (_components._actorView != null)
        {
            _components._actorView.PlayHurtAnimation();
        }
    }
    private void SetDeadOnAnimation()
    {
        if (_components._actorView != null)
        {
            _components._actorView.PlayDeathAnimation();
        }
    }

    private void Dead()
    {
        SetStunOnAnimation();
        SetDeadOnAnimation();
        if(_components._zombieAI != null)
            _components._zombieAI.OffNavMeshCollider();
        _components._collider.enabled = false;
    }
}