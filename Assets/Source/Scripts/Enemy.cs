using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SerializeField] private Transform _holeDetector;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private LayerMask _platformLayerIndex;

    private readonly float _holeDistance = 0.1f;

    private AnimationState _state = AnimationState.Walking;
    private float _direction = -1;

    private void Awake()
    {
        _skeletonAnimation.Initialize(true);
    }

    private void Update()
    {
        ApplyWalkActions();
        SwitchAnimation();
    }

    private void ApplyWalkActions()
    {
        Move();
        _state = AnimationState.Walking;
    }

    private void SwitchAnimation()
    {
        int stateIndex = 0;
        string currentAnimationName = SetAnimationName();

        if (_skeletonAnimation.AnimationName != currentAnimationName)
        {
            _skeletonAnimation.AnimationState.SetAnimation(stateIndex, currentAnimationName, true);
        }
    }

    private bool IsGroundAhead()
    {
        return Physics2D.Raycast(_holeDetector.position, Vector2.down, _holeDistance, _platformLayerIndex);
    }

    private void CalculateDirection()
    {
        if (IsGroundAhead() == false)
        {
            _direction *= -1;
        }
    }

    private string SetAnimationName()
    {
        string walkAnimatonName = "Walk";
        string emptyName = "";

        if (_state == AnimationState.Walking)
        {
            return walkAnimatonName;
        }

        return emptyName;
    }

    private void Move()
    {
        CalculateDirection();

        transform.position += _walkSpeed * _direction * Time.deltaTime * Vector3.right;

        SetOrientation();
    }

    private void SetOrientation()
    {
        switch (_direction)
        {
            case > 0:
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                break;
            case < 0:
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                break;
        }
    }
}
