using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
internal class PlayerMovementComponents
{
    [SerializeField] internal CharacterController _characterController;
    [SerializeField] internal ActorView _actorView;
}

public class PlayerMovement : PlayerInput
{
    [SerializeField] private float _speed;
    [SerializeField] private PlayerMovementComponents _components;

    public bool IsControlCharacter { get; set; } = true;

    private float _nVelocity;
    private float _normalSpeed;
    RaycastHit _rayHit;

    private void Awake()
    {
        _normalSpeed = _speed;
    }

    private void Update()
    {
        float horizontal = InputHorizontal;
        float vertical = InputVertical;

        Vector3 direction = Vector3.right * horizontal + Vector3.forward * vertical;
        Vector3 directionForCharacter = transform.right * horizontal + transform.forward * vertical;

        if (IsControlCharacter && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _rayHit))
        {
            transform.LookAt(new Vector3(_rayHit.point.x, transform.position.y, _rayHit.point.z));
        }
        if (direction.magnitude != 0 && IsControlCharacter)
        {
            _components._characterController.Move(direction.normalized * (_speed * Time.deltaTime));

            SetRunAnimationDirectionForCharacter(direction, directionForCharacter);
        }
        else
        {
            _components._actorView.SetAnimationForwardValue(0);
            _components._actorView.SetAnimationRightwardValue(0);
        }
    }

    private void SetRunAnimationDirectionForCharacter(Vector3 direction, Vector3 directionForCharacter)
    {
        directionForCharacter = directionForCharacter.normalized;
        direction = direction.normalized;

        if (direction.x > 0.5f)
        {
            InvertNumber(ref directionForCharacter.z);
            SetRunAnimationForDirection(directionForCharacter);
        }
        if (direction.x < -0.5f)
        {
            InvertNumber(ref directionForCharacter.z);
            SetRunAnimationForDirection(directionForCharacter);
        }
        if (direction.z > 0.5f)
        {
            InvertNumber(ref directionForCharacter.x);
            SetRunAnimationForDirection(directionForCharacter);
        }
        if (direction.z < -0.5f)
        {
            InvertNumber(ref directionForCharacter.x);
            SetRunAnimationForDirection(directionForCharacter);
        }
    }

    private void SetRunAnimationForDirection(Vector3 direction)
    {
        _components._actorView.SetAnimationForwardValue(direction.z);
        _components._actorView.SetAnimationRightwardValue(direction.x);
    }

    private void InvertNumber(ref float number)
    {
        number /= -1;
    }

    public void ApplyBoost(int boost, int timeBoost)
    {
        if (boost < 0)
            throw new ArgumentOutOfRangeException(gameObject.name + " - Apply boost < 0");
        else
        {
            _speed += boost;
            StartCoroutine(SetNormalSpeed(timeBoost));
        }
    }

    private IEnumerator SetNormalSpeed(int second)
    {
        yield return new WaitForSeconds(second);
        _speed = _normalSpeed;
    }

    public void SetIsControlCharacterFalse()
    {
        IsControlCharacter = false;
    }

    public void SetIsControlCharacterTrue()
    {
        IsControlCharacter = true;
    }
}