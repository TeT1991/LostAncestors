using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class EntityCharacter : Entity
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private float _jumpPower;

    private readonly string _walkAnimationName = "Walk";
    private readonly string _jumpAnimationName = "Jump_up";
    private readonly string _idleAnimationName = "Idle";

    private EntityJumper _entityJumper;
    private EntityRotater _entityRotater;

    private List<ButtonType> _commands;


    protected override void Update()
    {
        base.Update();
    }

    protected override void Init()
    {
        base.Init();

        _entityJumper = new(_rigidBody);
        _entityRotater = new(this);
        _commands = new();

        _inputReader.OnButtonPressed += AddCommand;
        _inputReader.OnButtonReleased += RemoveCommand;

        _groundDetector.OnGroundDetected += _entityJumper.TryAllowJump;
    }

    private void AddCommand(ButtonType buttonType)
    {
        if (_commands.Contains(buttonType) == false)
        {
            if (buttonType == ButtonType.Jump)
            {
                if (_entityJumper.CanJump)
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
                _entityRotater.Rotate(direction);
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

        if (_commands.Contains(ButtonType.Jump) && _entityJumper.CanJump)
        {
            Jump();
        }

        SetAnimationByAction();
    }

    private void Jump()
    {
        _entityJumper.Jump(_jumpPower);
    }

    private void SetAnimationByAction()
    {
        if (_commands.Count > 0)
        {
            if (_entityJumper.CanJump == false)
            {
                AnimationSwitcher.SetAnimation(_jumpAnimationName, true);
                return;
            }

            else if (_commands[0] == ButtonType.Walk_right || _commands[0] == ButtonType.Walk_left)
            {
                AnimationSwitcher.SetAnimation(_walkAnimationName, true);
                return;
            }
        }
        else
        {
            AnimationSwitcher.SetAnimation(_idleAnimationName, true);
        }
    }

    private void StartMove(int direction)
    {
        EntityMover.SetDirection(direction);
        EntityMover.AllowMove();
    }

    private void StopMove()
    {
        EntityMover.DenyMove();
    }
}