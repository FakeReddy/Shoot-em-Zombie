using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public bool FollowTarget { get; set; } = true;

    [SerializeField] private Transform _target;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private void Update()
    {
        if (_navMeshAgent.enabled == true && FollowTarget == true && _target != null)
        {
            _navMeshAgent.SetDestination(_target.position);
        }
        else if(_navMeshAgent.enabled == true && FollowTarget == false)
        {
            _navMeshAgent.SetDestination(transform.position);
        }
    }
    public void SetTarget(Transform Target)
    {
        if (Target != null)
        {
            _target = Target;
        }
    }
    public void SetFollowTargetTrue()
    {
        FollowTarget = true;
    }
    public void SetFollowTargetFalse()
    {
        FollowTarget = false;
    }

    public void OffNavMeshCollider()
    {
        _navMeshAgent.enabled = false;
    }
}