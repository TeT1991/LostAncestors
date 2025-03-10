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
        _character.Attacker.ApplyAttack(_character.DirectionSwitcher.Direction);  
    }

    public void SetAttackInfo(Projectile projectile, Transform projectileLaunchPoint, string animationName)
    {
        _character.Attacker.SetProjectile(projectile, projectileLaunchPoint);
        _character.AnimationSwitcher.TrySetAnimation(animationName, false);
    }
}
