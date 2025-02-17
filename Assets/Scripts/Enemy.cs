using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Attacker), typeof(Patroler))]
[RequireComponent(typeof(CollideDetector), typeof(Attacker), typeof(Patroler))]
[RequireComponent(typeof(DirectionSwitcher), typeof(Attacker), typeof(Patroler))]
public class Enemy : Entity
{
    private CharacterDetector _characterDetector;

    protected override void Init()
    {
        base.Init();

        CollideDetector = GetComponent<CollideDetector>();
        Mover = GetComponent<Mover>();
        Attacker = GetComponent<Attacker>();
        Patroler = GetComponent<Patroler>();
        _characterDetector = GetComponent<CharacterDetector>();

        Attacker.Init(_projectile, _reloadTime);
        Patroler.Init(CollideDetector, 1);
        _characterDetector.Init(DirectionSwitcher.Direction);

        CollideDetector.ObstacleCollided += DirectionSwitcher.ReverseDirection;
        DirectionSwitcher.DirectionChanged += FlipSprites;
        DirectionSwitcher.DirectionChanged += _characterDetector.SetDirection;

        _currentState = EntityStates.Patroling;

        Conditions = new List<StateConditions>
        {
            new EnemyRangeAttackConditions(Attacker,_characterDetector),
            new PatrolingStateConditions(_characterDetector),
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
            case EntityStates.Patroling:
                ApplyPatrolingStateActions();
                break;

            case EntityStates.RangeAttack:
                ApplyRangeAttackStateActions();
                break;
        }
    }
}
