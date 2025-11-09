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
    [SerializeField] private PickableDetector _pickableDetector;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpPower;
    private int _maxHealth = 3;
    private int _health = 1;

    private Jumper _jumper;
    private Rotater _rotater;
    private Mover _mover;
    private AnimationSwitcher _animationSwitcher;

    private bool _canMove;
    private bool _canJump;

    private List<ButtonType> _commands;

    private void Awake()
    {
        Init();
    }

    private void OnDestroy()
    {
        _inputReader.OnButtonPressed -= AddCommand;
        _inputReader.OnButtonReleased -= RemoveCommand;

        _groundDetector.OnGroundDetected -= AllowJump;
        _groundDetector.OnGroundNotDetected -= DenyJump;

        _pickableDetector.OnPicked -= TryPickUp;
    }

    private void FixedUpdate()
    {
        Move();
        ChangeAnimationByAction();
    }

    private void Init()
    {
        _mover = new(_rigidBody, _moveSpeed);
        _jumper = new(_rigidBody, _jumpPower);
        _rotater = new(gameObject.transform);
        _animationSwitcher = new(_skeletonAnimation);
        _commands = new();

        _inputReader.OnButtonPressed += AddCommand;
        _inputReader.OnButtonReleased += RemoveCommand;

        _groundDetector.OnGroundDetected += AllowJump;
        _groundDetector.OnGroundNotDetected += DenyJump;

        _pickableDetector.OnPicked += TryPickUp;
        
    }

    private void AddCommand(ButtonType buttonType)
    {
        if (_commands.Contains(buttonType) == false)
        {
            if (buttonType == ButtonType.Jump)
            {
                if (_canJump)
                {
                    _commands.Insert(0, buttonType);
                }
            }
            else
            {
                _commands.Insert(0, buttonType);
            }
        }

        ExecuteCommand();
    }

    private void RemoveCommand(ButtonType buttonType)
    {
        _commands.Remove(buttonType);

        ExecuteCommand();
    }

    private void ExecuteCommand()
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

        if (_commands.Contains(ButtonType.Jump) && _canJump)
        {
            Jump();
        }
    }

    private void ChangeAnimationByAction()
    {
        if (_canJump == false)
        {
            _animationSwitcher.PlayJumpAnimation();
            return;
        }

        bool walking = _commands.Contains(ButtonType.Walk_right) ||
                       _commands.Contains(ButtonType.Walk_left);

        if (walking)
        {
            _animationSwitcher.PlayWalkAnimation();
        }
        else
        {
            _animationSwitcher.PlayIdleAnimation();
        }
    }

    private void TryPickUp(IPickable pickable)
    {
        if (pickable.GetPickableType() == PickableType.Medkit)
        {
            if (_health < _maxHealth)
            {
                IncreaseHealth();
                pickable.PickUp();

            }
        }
    }

    private void StartMove(int direction)
    {
        _mover.SetDirection(direction);
        _canMove = true;
    }

    private void StopMove()
    {
        _canMove = false;
    }

    private void Move()
    {
        if (_canMove)
        {
            _mover.Move();
        }
    }

    private void AllowJump()
    {
        _canJump = true;
    }

    private void DenyJump()
    {
        _canJump = false;
    }

    private void Jump()
    {
        if (_canJump)
        {
            _jumper.Jump();
            DenyJump();
        }
    }

    private void IncreaseHealth()
    {
        _health++;
    }

    private void DecreaseHealth()
    {
        _health--;
    }
}