using System.Collections;
using UnityEngine;
using System;

[System.Serializable]
internal class Pistol
{
    [SerializeField] internal int _damage;
    [SerializeField] internal int _currentCountBullets;
    [SerializeField] internal int _maxCountBullets;
    [SerializeField] internal int _coolDown;
    [SerializeField] internal int _countShootBullets;
}

[System.Serializable]
internal class WeaponComponents
{
    [SerializeField] internal ActorView _actorView;
    [SerializeField] internal SoundListener _soundListener;
    [SerializeField] internal Spawner _spawner;
    [SerializeField] internal UiView _uiView;
    [SerializeField] internal GameData _gameData;
}

public class Weapon : PlayerInput
{
    [SerializeField] private Pistol _pistol;
    [SerializeField] private WeaponComponents _components;
    [SerializeField] private Transform _originStartBullet;
    [SerializeField] private LayerMask _shootedMask;

    private bool _isTakeDamage = true;

    private Health _zombie;
    private RaycastHit _rayHit;

    private void Update()
    {
        if (InputFire == true && _isTakeDamage == true)
        {
            _isTakeDamage = false;
            StartCoroutine(UnCoolDownFire());

            _components._actorView.PlayAttackAnimation();

            if (_pistol._currentCountBullets >= _pistol._countShootBullets)
            {
                _components._soundListener.PlaySoundShootWithPistol();
                _pistol._currentCountBullets -= _pistol._countShootBullets;
                _components._uiView.Bullets();

                FireFromWeapon();
            }
            else
                _pistol._currentCountBullets = 0;
        }
    }

    private void FireFromWeapon()
    {
        if (Physics.Raycast(_originStartBullet.position, _originStartBullet.forward, out _rayHit, _shootedMask) && _rayHit.transform.gameObject.TryGetComponent<Health>(out _zombie))
        {
            _zombie.ApplyDamage(_pistol._damage);

            if (_zombie.IsAlive == false)
            {
                if (_rayHit.transform.gameObject.TryGetComponent<MyZombieComponent>(out MyZombieComponent zombieComponent))
                {
                    Debug.Log(zombieComponent.AddScoreToKill);
                    _components._gameData.AddToCurrentScore(zombieComponent.AddScoreToKill);
                    _components._uiView.Scores();
                }

                CheckDeadZombiesOnScene();
            }
        }
    }

    private bool CheckDeadZombiesOnScene()
    {
        int countAliveEnemiesOnScene = 0;
        foreach (var zombiesOnScene in _components._spawner.ZombiesOnScene)
        {
            if (zombiesOnScene != null && zombiesOnScene.TryGetComponent<Health>(out _zombie))
            {
                if (_zombie.IsAlive == true)
                    break;
                else
                {
                    countAliveEnemiesOnScene++;
                    continue;
                }
            }
        }
        if (countAliveEnemiesOnScene >= _components._spawner.ZombiesOnScene.Count)
        {
            _components._spawner.StartSpawnZombies();
            return true;
        }
        else
            return false;
    }

    private IEnumerator UnCoolDownFire()
    {
        yield return new WaitForSeconds(_pistol._coolDown);
        _isTakeDamage = true;
    }

    public int GetCurrentBullets()
    {
        return _pistol._currentCountBullets;
    }

    public int GetMaxBullets()
    {
        return _pistol._maxCountBullets;
    }

    public void ApplyBullets(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(gameObject.name + " - Weapon ApplyBullets: count < 0");

        else if (count > _pistol._maxCountBullets)
            _pistol._currentCountBullets = _pistol._maxCountBullets;

        else if (_pistol._currentCountBullets >= _pistol._maxCountBullets)
            _pistol._currentCountBullets = _pistol._maxCountBullets;

        else if (_pistol._currentCountBullets >= _pistol._maxCountBullets - count)
            _pistol._currentCountBullets = _pistol._maxCountBullets;

        else
        {
            _components._soundListener.PlaySoundReloadPistol();
            _pistol._currentCountBullets += count;
        }
    }
}