using UnityEngine;

[System.Serializable]
class PlayerCollisionsComponents
{
    [SerializeField] internal PlayerMovement _playerMovement;
    [SerializeField] internal CameraFollow _cameraFollow;
    [SerializeField] internal Weapon _weapon;
    [SerializeField] internal Health _health;
    [SerializeField] internal UiView _uiView;
}
public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private PlayerCollisionsComponents _components;
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<MyBoxBulletComponent>(out MyBoxBulletComponent boxBulletComponent))
        {
            if (_components._weapon.GetCurrentBullets() < _components._weapon.GetMaxBullets())
            {
                Destroy(boxBulletComponent.gameObject);
                _components._weapon.ApplyBullets(boxBulletComponent.TakeBullet);
            }
            _components._uiView.Bullets();
        }
        if (other.TryGetComponent<MyHeartComponent>(out MyHeartComponent heartComponent))
        {
            if (_components._health.CurrentHealth < _components._health.MaxHealth)
            {
                Destroy(heartComponent.gameObject);
                _components._health.ApplyHeal(heartComponent.TakeHeal);
            }
            _components._uiView.Health();
        }
        if (other.TryGetComponent<MyBoostSkillComponent>(out MyBoostSkillComponent boostSkillComponent))
        {
            if (boostSkillComponent.Sphere.activeSelf)
            {
                boostSkillComponent.Sphere.SetActive(false);
                _components._playerMovement.ApplyBoost(boostSkillComponent.TakeBoost, boostSkillComponent.TimeBoost);
            }
        }
        if (other.TryGetComponent<MyVisionSkillComponent>(out MyVisionSkillComponent visionSkillComponent))
        {
            if (visionSkillComponent.Sphere.activeSelf)
            {
                visionSkillComponent.Sphere.SetActive(false);
                _components._cameraFollow.ApplyVision(visionSkillComponent.RangeVision, visionSkillComponent.TimeVision);
            }
        }
    }
}