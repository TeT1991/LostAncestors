using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SerializeField] private GroundDetector _groundDetector;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpPower;

    private EntityJumper _jumper;
    private Rotater _rotater;
    private Mover _mover;
    private AnimationSwitcher _animationSwitcher;

    private List<ButtonType> _commands;

    private void Awake()
    {
        Init();
    }

    private void FixedUpdate()
    {
        _mover.Move(_moveSpeed);
        SetAnimationByAction();
    }

    private void Init()
    {
        _mover = new(_rigidBody);
        _jumper = new(_rigidBody);
        _rotater = new(gameObject.transform);
        _animationSwitcher = new(_skeletonAnimation);
        _commands = new();

        _inputReader.OnButtonPressed += AddCommand;
        _inputReader.OnButtonReleased += RemoveCommand;

        _groundDetector.OnGroundDetected += _jumper.AllowJump;
        _groundDetector.OnGroundNotDetected += _jumper.DenyJump;
    }

    private void AddCommand(ButtonType buttonType)
    {
        if (_commands.Contains(buttonType) == false)
        {
            if (buttonType == ButtonType.Jump)
            {
                if (_jumper.CanJump)
                {
                    _commands.Insert(0, buttonType);
                }
            }
            else
            {
                _commands.Insert(0, buttonType);
            }
        }

        ReactOnInput();
    }

    private void RemoveCommand(ButtonType buttonType)
    {
        _commands.Remove(buttonType);

        ReactOnInput();
    }

    private void ReactOnInput()
    {
        int direction = 0;

        if (_commands.Count > 0)
        {
            if (_commands.Contains(ButtonType.Walk_right))
            {
                direction = 1;
            }
            else if (_commands.Contains(ButtonType.Walk_left))
            {
                direction = -1;
            }

            if (direction != 0)
            {
                StartMove(direction);
                _rotater.Rotate(direction);
            }
            else
            {
                StopMove();
            }
        }
        else
        {
            StopMove();
        }

        if (_commands.Contains(ButtonType.Jump) && _jumper.CanJump)
        {
            Jump();
        }

        //SetAnimationByAction();
    }

    private void Jump()
    {
        _jumper.Jump(_jumpPower);
    }

    private void SetAnimationByAction()
    {
        if (_jumper.CanJump == false)
        {
            _animationSwitcher.SetJumpAnimation();
            return;
        }

        bool walking = _commands.Contains(ButtonType.Walk_right) ||
                       _commands.Contains(ButtonType.Walk_left);

        if (walking)
        {
            _animationSwitcher.SetWalkAnimation();
        }
        else
        {
            _animationSwitcher.SetIdleAnimation();
        }
    }

    private void StartMove(int direction)
    {
        _mover.SetDirection(direction);
        _mover.AllowMove();
    }

    private void StopMove()
    {
        _mover.DenyMove();
    }
}