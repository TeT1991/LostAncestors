using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    protected override void Init()
    {
        base.Init();

        Mover = GetComponent<Mover>();
        Jumper = GetComponent<Jumper>();
        CollideDetector = GetComponent<CollideDetector>();
        Attacker = GetComponent<Attacker>();
        DirectionSwitcher = GetComponent<DirectionSwitcher>();

        Jumper.Init(GetComponent<Rigidbody2D>(), _jumpPower);
        Attacker.Init(_projectile, _reloadTime);

        CollideDetector.PlatformCollided += Jumper.SetStatus;

        _currentState = EntityStates.Idle;

        Conditions = new List<StateConditions>
        {
            new IdleStateConditions(CollideDetector),
            new WalkStateConditions(),
            new JumpStateConditions(Jumper),
            new RangeAttackConditions(Attacker)
        };
    }

    protected override void Update()
    {
        base.Update();
        _textMeshPro.text = _currentState.ToString();

    }

    protected override void ApplyStateActions()
    {
        switch (_currentState)
        {
            case EntityStates.Idle:
                break;

            case EntityStates.Walk:
                ApplyWalkStateActions();
                break;

            case EntityStates.Jump:
                ApplyJumpStateActions();
                break;

            case EntityStates.RangeAttack:
                ApplyRangeAttackStateActions();
                break;
        }
    }

    protected override void ApplyWalkStateActions()
    {
        DirectionSwitcher.SetDirection(Input.GetAxis("Horizontal"));
        Mover.Move(GroundSpeed * DirectionSwitcher.Direction);
    }

    protected override void ApplyJumpStateActions()
    {
        Jumper.Jump();
        DirectionSwitcher.SetDirection(Input.GetAxis("Horizontal"));
        Mover.Move(_airHorizontalSpeed * Input.GetAxis("Horizontal"));
    }
}
