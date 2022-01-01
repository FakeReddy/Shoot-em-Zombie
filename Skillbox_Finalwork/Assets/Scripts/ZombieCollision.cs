using System.Collections;
using UnityEngine;

[System.Serializable]
internal class ZombieCollisionsComponents
{
    [SerializeField] internal ActorView _actorView;
    [SerializeField] internal ZombieAI _zombieAI;
    [SerializeField] internal Health _health;
    [SerializeField] internal UiView _uiView;
}

[System.Serializable]
internal class ZombieCollisionsInfo
{
    [SerializeField] internal int _damage;
    [SerializeField] internal int _coolDown;
}

public class ZombieCollision : MonoBehaviour
{
    [SerializeField] private ZombieCollisionsComponents _components;
    [SerializeField] private ZombieCollisionsInfo _info;

    [SerializeField] private bool _isTakeDamage;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<MyPlayerComponent>(out MyPlayerComponent component) && other.TryGetComponent<Health>(out Health healthTarget))
        {
            if (_isTakeDamage == true && _components._zombieAI != null && _components._health.IsAlive == true)
            {
                StartCoroutine(UnCoolDownFromAttack());
                healthTarget.ApplyDamage(_info._damage);

                _components._zombieAI.FollowTarget = false;
                _components._actorView.PlayAttackAnimation();

                _components._uiView.Health();
                _isTakeDamage = false;
            }
        }
    }

    public void SetUiView(UiView uiView)
    {
        _components._uiView = uiView;
    }

    private IEnumerator UnCoolDownFromAttack()
    {
        yield return new WaitForSeconds(_info._coolDown);
        _isTakeDamage = true;
    }
}