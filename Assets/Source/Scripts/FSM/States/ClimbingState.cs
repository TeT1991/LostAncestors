using System;
using UnityEngine;

public class ClimbingState : EntityState
{
    private readonly Character _character;
    private readonly Mover _mover;

    private float _climbingSpeed = 3;
    private readonly bool _isClimbing; // ”дал€ем локальную переменную
    private readonly Func<bool> _isClimbingGetter; // ƒобавл€ем ссылку на метод получени€ значени€

    public ClimbingState(Entity entity, StateMachine stateMachine, Func<bool> isClimbingGetter) : base(entity, stateMachine)
    {
        _character = entity as Character;
        _mover = _character.Mover;
        _isClimbingGetter = isClimbingGetter; // —охран€ем ссылку на метод получени€ значени€
    }

    public override void Enter()
    {
        SetGravity(0);
    }

    public override void FrameUpdate()
    {
        _mover.MoveVecrtiacal(_climbingSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
    }

    public override void Exit()
    {
        SetGravity(1);
    }

    private void SetGravity(float value)
    {
        _character.RigidBody.gravityScale = value;
    }
}