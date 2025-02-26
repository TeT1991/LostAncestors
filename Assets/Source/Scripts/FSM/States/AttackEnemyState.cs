using UnityEngine;

public class AttackEnemyState : EntityState
{
    private readonly Enemy _enemy;
    public AttackEnemyState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
        _enemy = entity as Enemy;
    }

    public override void Enter()
    {
        string animationName = "Attack";
        _enemy.AnimationSwitcher.TrySetAnimation(animationName, true);
        _enemy.Attacker.SetProjectile(_enemy.Projectile, _enemy.ProjectileLaunchPoint);
    }

    public override void FrameUpdate()
    {
        _enemy.Attacker.ApplyAttack(_enemy.DirectionSwitcher.Direction);
    }
}
