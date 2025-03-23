using UnityEngine;

public class ClimbingState : EntityState
{
    private readonly Character _character;
    private readonly Mover _mover;

    private float _climbingSpeed = 3;

    public ClimbingState(Entity enity, StateMachine stateMachine) : base(enity, stateMachine)
    {
        _character = enity as Character;
        _mover = _character.Mover;
    }

    public override void Enter()
    {
        _character.RigidBody.bodyType = RigidbodyType2D.Static;
    }

    public override void FrameUpdate()
    {
        _mover.MoveVecrtiacal(_climbingSpeed * Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Space))
        {
            Exit();
        }
    }

    public override void Exit()
    {
        _character.RigidBody.bodyType = RigidbodyType2D.Dynamic;
    }
}
