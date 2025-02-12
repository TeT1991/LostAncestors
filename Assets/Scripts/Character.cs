using System.Collections.Generic;
using UnityEngine;

[RequireComponent((typeof(Attacker)), (typeof(Mover)), typeof(Jumper))]
[RequireComponent((typeof(CollideDetector)),(typeof(Rigidbody2D)))]
public class Character : MonoBehaviour
{
    [SerializeField] private float _groundSpeed = 1;
    [SerializeField] private float _airHorizontalSpeed = 1;
    [SerializeField] private float _jumpPower = 1;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private TMPro.TextMeshProUGUI _textMeshPro;
    private bool _isGrounded;

    private EntityStates _currentState = EntityStates.Idle;
    private List<StateConditions> _conditions;

    private Mover _mover;
    private Jumper _jumper;
    private CollideDetector _collideDetector;
    private Attacker _attacker;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _collideDetector = GetComponent<CollideDetector>();
        _attacker = GetComponent<Attacker>();

        _jumper.Init(GetComponent<Rigidbody2D>(), _jumpPower);
        _attacker.Init(_projectile);

        _collideDetector.Collided += _jumper.SetStatus;

        _conditions = new List<StateConditions>
        {
            new IdleStateConditions(_collideDetector),
            new WalkStateConditions(),
            new JumpStateConditions(_jumper)
        };

    }

    private void Update()
    {
        SwitchState();
        ApplyStateActions();
        _textMeshPro.text = _currentState.ToString();
    }

    private void SwitchState()
    {
        foreach (var condition in _conditions)
        {
            if (condition.CanChange(_currentState))
            {
                _currentState = condition.Type;
                return;
            }
        }
    }

    private void ApplyStateActions()
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

    private void ApplyWalkStateActions()
    {
        _mover.Move(_groundSpeed * Input.GetAxis("Horizontal"));
    }

    private void ApplyJumpStateActions()
    {
        _jumper.Jump();
        _mover.Move(_airHorizontalSpeed * Input.GetAxis("Horizontal"));
    }

    private void ApplyRangeAttackStateActions()
    {
        _attacker.ApplyRangeAttack();
    }
}
