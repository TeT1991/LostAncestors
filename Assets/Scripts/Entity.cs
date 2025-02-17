using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float GroundSpeed = 0.5f;
    public float _airHorizontalSpeed = 1;
    public float _jumpPower = 1;
    public GameObject _projectile;
    public float _reloadTime = 1;

    public TMPro.TextMeshProUGUI _textMeshPro;

    public EntityStates _currentState = EntityStates.Idle;
    public List<StateConditions> Conditions { get; protected set; }

    public Mover Mover { get; set; }
    public Jumper Jumper { get; set; }
    public CollideDetector CollideDetector { get; set; }
    public Attacker Attacker { get; set; }
    public Patroler Patroler { get; set; }

    public DirectionSwitcher DirectionSwitcher { get; set; }

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        Init();
    }

    protected virtual void Update()
    {
        SwitchState();
        ApplyStateActions();
    }

    protected virtual void Init()
    {
        //Mover = GetComponent<Mover>();
        //Jumper = GetComponent<Jumper>();
        //CollideDetector = GetComponent<CollideDetector>();
        //Attacker = GetComponent<Attacker>();
        //Patroler = GetComponent<Patroler>();

        //Jumper.Init(GetComponent<Rigidbody2D>(), _jumpPower);
        //Attacker.Init(_projectile, _reloadTime);
        //Patroler.Init(CollideDetector);
        DirectionSwitcher = GetComponent<DirectionSwitcher>();
        DirectionSwitcher.DirectionChanged += FlipSprites;

        _spriteRenderer = GetComponent<SpriteRenderer>();

        _currentState = EntityStates.Idle;
 

        Conditions = new List<StateConditions>
        {
        };
    }

    protected void SwitchState()
    {
        foreach (var condition in Conditions)
        {
            if (condition.CanChange(_currentState))
            {
                _currentState = condition.Type;
                return;
            }
        }
    }

    protected virtual void ApplyStateActions()
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

            case EntityStates.Patroling:
                ApplyPatrolingStateActions();
                break;

        }
    }

    protected virtual void ApplyWalkStateActions()
    {

    }

    protected virtual void ApplyJumpStateActions()
    {
    }

    protected virtual void ApplyRangeAttackStateActions()
    {
        Attacker.ApplyRangeAttack(DirectionSwitcher.Direction);
    }

    protected virtual void ApplyPatrolingStateActions()
    {
        Mover.Move(GroundSpeed * DirectionSwitcher.Direction);
    }

    protected void FlipSprites(float direction)
    {
        bool canFlip = (direction <= 0);

        _spriteRenderer.flipX = canFlip;
    }
}
