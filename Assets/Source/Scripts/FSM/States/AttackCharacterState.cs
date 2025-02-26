using UnityEngine;

public class AttackCharacterState : EntityState
{
    private readonly Character _character;

    public AttackCharacterState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
        _character = entity as Character;
    }

    public override void Enter()
    {
        string animationName = "RangeAttack";

        _character.Attacker.SetProjectile(_character.Projectile, _character.ProjectileLaunchPoint);
        _character.Attacker.ApplyAttack(_character.DirectionSwitcher.Direction);
        _character.AnimationSwitcher.TrySetAnimation(animationName, false);
    }
}
