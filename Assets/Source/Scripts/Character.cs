using Spine.Unity;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SerializeField] private Detector _groundDetector;
    [SerializeField] private Rotater _rotater;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _walkSpeed;

    private Rigidbody2D _rigidbody;
    private AnimationState _state;
    private InputReader _inputReader;

    private bool _isGrounded = true;

    private void Awake()
    {
        _skeletonAnimation.Initialize(true);
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundDetector.OnCollided += SetIsGrounded;
        _inputReader = new InputReader();
    }

    private void Update()
    {
        ApplyActions(_inputReader.GetPressedButton());
        SwitchAnimation();
    }

    private void ApplyActions(KeyCode pressedButton)
    {
        {
            if (pressedButton == KeyCode.Space || _isGrounded == false)
            {
                ApplyJumpActions();
                SwitchAnimation();
                _state = AnimationState.Jumping;
                return;
            }

            if (pressedButton == KeyCode.D || pressedButton == KeyCode.A)
            {
                ApplyWalkActions();
                _state = AnimationState.Walking;
                return;
            }

            if (pressedButton == KeyCode.None)
            {
                ApplyIdleActions();
                _state = AnimationState.Idle;
            }
        }
    }

    private void ApplyJumpActions()
    {
        Jump();
        Move();
        _state = AnimationState.Jumping;
    }

    private void ApplyWalkActions()
    {
        Move();
        _state = AnimationState.Walking;
    }

    private void ApplyIdleActions()
    {
        _state = AnimationState.Idle;
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

    private string SetAnimationName()
    {
        string upAnimationName = "Jump_up";
        string downAnimationName = "Jump_down";
        string idleAnimationName = "Idle";
        string walkAnimatonName = "Walk";
        string emptyName = "";

        if (_state == AnimationState.Jumping)
        {
            if (_rigidbody.velocity.y > 0)
            {
                return upAnimationName;
            }

            if (_rigidbody.velocity.y < 0)
            {
                return downAnimationName;
            }
        }

        if (_state == AnimationState.Walking)
        {
            return walkAnimatonName;
        }

        if (_state == AnimationState.Idle)
        {
            return idleAnimationName;
        }

        return emptyName;
    }

    private void Move()
    {
        transform.position += _walkSpeed * Input.GetAxis("Horizontal") * Time.deltaTime * Vector3.right;

        _rotater.Rotate(Input.GetAxis("Horizontal"));
    }

    private void Jump()
    {
        if ((Mathf.Abs(_rigidbody.velocity.y) == 0))
        {
            _rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
    }

    private void SetIsGrounded(bool isGrounded)
    {
        _isGrounded = isGrounded;
    }
}
