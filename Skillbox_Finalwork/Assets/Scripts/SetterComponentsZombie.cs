using UnityEngine;

[System.Serializable]
internal class SetterComponentsZombieComponents
{
    [SerializeField] internal ZombieAI _zombieAi;
    [SerializeField] internal ZombieCollision _zombieCollision;
    [SerializeField] internal Health _health;
}
public class SetterComponentsZombie : MonoBehaviour
{
    [SerializeField] private SetterComponentsZombieComponents _components;
    public void SetDestinationZombie(Transform target)
    {
        _components._zombieAi.SetTarget(target);
    }
    public void SetUiViewZombie(UiView uiView)
    {
        _components._zombieCollision.SetUiView(uiView);
    }
}