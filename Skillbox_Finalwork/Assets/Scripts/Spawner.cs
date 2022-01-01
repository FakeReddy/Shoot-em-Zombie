using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
internal class Zombie
{
    [SerializeField] internal int _count;
    [SerializeField] internal Transform _targetEnemies;
    [SerializeField] internal Transform _pointSpawnEnemy;
    [SerializeField] internal SetterComponentsZombie _prefab;
}

[System.Serializable]
internal class SpawnerComponents
{
    [SerializeField] internal UiView _uiView;
}

[System.Serializable]
internal class SpawnerInfoBoxBullet
{
    [SerializeField] internal float _spawnTimeBoxBullet;
    [SerializeField] internal GameObject _prefab;
    [SerializeField] internal Transform _spawnPointBox;
}

[System.Serializable]
internal class SpawnerInfoHeart
{
    [SerializeField] internal float _spawnTimeHeart;
    [SerializeField] internal GameObject _prefab;
    [SerializeField] internal Transform _spawnPointHeart;
}

[System.Serializable]
internal class SpawnerInfoBoostSkill
{
    [SerializeField] internal float _timeToActivate;
    [SerializeField] internal GameObject _sphere;
}

[System.Serializable]
internal class SpawnerInfoVisionSkill
{
    [SerializeField] internal float _timeToActivate;
    [SerializeField] internal GameObject _sphere;
}

[System.Serializable]
internal class SpawnerTimer
{
    internal bool Timer(float seconds, ref float progress)
    {
        progress += Time.deltaTime;
        if (progress >= seconds)
        {
            progress = 0;
            return true;
        }
        else
            return false;
    }
}

public class Spawner : GameData
{
    [SerializeField] private List<Zombie> _zombies;
    [SerializeField] private List<GameObject> _zombiesOnScene;

    [SerializeField] private SpawnerComponents _components;
    [SerializeField] private SpawnerInfoBoxBullet _boxBullet;
    [SerializeField] private SpawnerInfoHeart _heart;
    [SerializeField] private SpawnerInfoBoostSkill _boostSkill;
    [SerializeField] private SpawnerInfoVisionSkill _visionSkill;
    [SerializeField] private float _timeToSpawnZombies;

    private SpawnerTimer _timer = new SpawnerTimer();

    private float _spawnBoxBulletProgressTimer;
    private float _spawnHeartProgressTimer;
    private float _activateBoostSkillProgressTimer;
    private float _activateVisionSkillProgressTimer;

    public List<GameObject> ZombiesOnScene => _zombiesOnScene;
    public int CountZombiesOnScene => _zombiesOnScene.Count;

    private void Awake()
    {
        StartSpawnZombies();
    }

    public void StartSpawnZombies()
    {
        StartCoroutine(DelaySpawnZombies());
    }

    private IEnumerator DelaySpawnZombies()
    {
        yield return new WaitForSeconds(_timeToSpawnZombies);

        DestroyDeathEnemiesOnScene();
        _zombiesOnScene = new List<GameObject>();

        InstantSpawnZombies();

        _currentRounds++;
        _components._uiView.Rounds();

        SaveGameData();
    }

    private void InstantSpawnZombies()
    {
        for (int i = 0; i < _zombies.Count; i++)
        {
            for (int j = 0; j < _zombies[i]._count; j++)
            {
                _zombies[i]._prefab.SetDestinationZombie(_zombies[i]._targetEnemies);
                _zombies[i]._prefab.SetUiViewZombie(_components._uiView);

                GameObject enemyObject = SpawnObject(_zombies[i]._prefab.gameObject, _zombies[i]._pointSpawnEnemy.position, Quaternion.identity);
                _zombiesOnScene.Add(enemyObject);
            }
        }
    }

    public void DestroyDeathEnemiesOnScene()
    {
        foreach (var zombieOnScene in _zombiesOnScene)
        {
            if (zombieOnScene.TryGetComponent<Health>(out Health zombie) && zombie.IsAlive == false)
                Destroy(zombie.gameObject);
        }
    }
    public void RemoveZombieListOnScene(GameObject zombie)
    {
        _zombiesOnScene.Remove(zombie);
    }

    private void Update()
    {
        if (_timer.Timer(_boostSkill._timeToActivate, ref _activateBoostSkillProgressTimer) && _boostSkill._sphere.activeSelf == false)
            _boostSkill._sphere.SetActive(true);
        if (_timer.Timer(_visionSkill._timeToActivate, ref _activateVisionSkillProgressTimer) && _visionSkill._sphere.activeSelf == false)
            _visionSkill._sphere.SetActive(true);

        SpawnedObject(_heart._spawnTimeHeart, ref _spawnHeartProgressTimer, _heart._prefab, _heart._spawnPointHeart.position);
        SpawnedObject(_boxBullet._spawnTimeBoxBullet, ref _spawnBoxBulletProgressTimer, _boxBullet._prefab, _boxBullet._spawnPointBox.position);
    }

    private void SpawnedObject(float seconds, ref float timerProgress, GameObject prefab, Vector3 point)
    {
        if (_timer.Timer(seconds, ref timerProgress))
            SpawnObject(prefab, point, Quaternion.identity);
    }

    public GameObject SpawnObject(GameObject entity, Vector3 point, Quaternion rotation)
    {
        return Instantiate(entity, point, rotation);
    }
}