using UnityEngine;
public class ActorView : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void SetAnimationForwardValue(float value)
    {
        _animator.SetFloat(GlobalStringsVars.ForwardValueAnim, value);
    }

    public void SetAnimationRightwardValue(float value)
    {
        _animator.SetFloat(GlobalStringsVars.RightwardValueAnim, value);
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(GlobalStringsVars.AttackAnim);
    }

    public void PlayHurtAnimation()
    {
        _animator.SetTrigger(GlobalStringsVars.HurtAnim);
    }

    public void PlayDeathAnimation()
    {
        _animator.SetTrigger(GlobalStringsVars.DeathAnim);
    }
}