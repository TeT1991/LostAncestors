using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SkeletonAnimation))]
public class Character : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _jumpPower;

    private SkeletonAnimation _skeleton;
    private Rigidbody2D _rigidbody;
    private EntityState _state;

    private bool _isJumpingStarted = false;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _skeleton = GetComponent<SkeletonAnimation>();
    }

    private void Update()
    {
        TrySwitchState();
        ApplyStateActions();
        Debug.Log(_state.ToString());
    }

    private void TrySwitchState()
    {
        if (_state != EntityState.Jumping && Input.GetKeyDown(KeyCode.Space) && _isJumpingStarted)
        {
            StartJump();
        }

        if (IsGrounded())
        {
            if (_state != EntityState.Jumping && _state != EntityState.Walking)
            {
                SetState(EntityState.Idle);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                SetState(EntityState.Walking);
            }
        }
    }

    private void SetState(EntityState state)
    {
        if (_state != state)
        {
            _state = state;
        }
    }

    private void ApplyStateActions()
    {
        switch (_state)
        {
            case EntityState.Jumping:
                ApplyJumpingStateActions();
                break;

            case EntityState.Walking:
                ApplyWalkingStateActions();
                break;

            case EntityState.Idle:
                ApplyIdleStateActions();
                break;
        }
    }

    private void ApplyJumpingStateActions()
    {
        string animationName = string.Empty;
        bool isAnimationLooping = true;

        if (_rigidbody.velocity.y > 0)
        {
            animationName = "Jump_up";
        }

        if (_rigidbody.velocity.y < 0)
        {
            animationName = "Jump_down";
        }

        if (animationName != string.Empty)
        {
            SwitchAnimation(animationName, isAnimationLooping);
        }

        MoveHorizontal();
    }

    private void ApplyWalkingStateActions()
    {
        string animationName = "Walk";
        bool isAnimationLooping = true;
        SwitchAnimation(animationName, isAnimationLooping);
        MoveHorizontal();
    }

    private void ApplyIdleStateActions()
    {
        string animationName = "Idle";
        bool isAnimationLooping = true;
        SwitchAnimation(animationName, isAnimationLooping);
    }

    private void MoveHorizontal()
    {
        transform.position += (Vector3)(CalculateHorizontalSpeed() * Time.deltaTime * Vector2.right);

        FlipSkeletonToDirection();
    }

    private float CalculateHorizontalSpeed()
    {
        return _walkSpeed * Input.GetAxis("Horizontal");
    }

    private void SwitchAnimation(string animationName, bool isLooping)
    {
        int animationTrackIndex = 0;

        if (_skeleton.AnimationName != animationName)
        {
            _skeleton.AnimationState.SetAnimation(animationTrackIndex, animationName, isLooping);
        }
    }

    private void StartJump()
    {
        _rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);

        SetState(EntityState.Jumping);
    }

    private void FlipSkeletonToDirection()
    {
        float direction = Input.GetAxis("Horizontal");

        if (direction > 0)
        {
            _skeleton.initialFlipX = false;
        }
        if (direction < 0)
        {
            _skeleton.initialFlipX = true;
        }
    }

    private bool IsGrounded()
    {
        float treshold = 0.01f;
        return Mathf.Abs(_rigidbody.velocity.y) < treshold;
    }
}

public enum EntityState
{
    Idle,
    Walking,
    Jumping
}

