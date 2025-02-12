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

        _collideDetector.CollisionChanged += _jumper.SetStatus;
    }

    private void Update()
    {
        SwitchState();
        ApplyStateActions();
        _textMeshPro.text = _currentState.ToString();
    }

    private void SwitchState()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _jumper.IsGrounded || _jumper.IsGrounded == false)
        {
            _currentState = EntityStates.Jump;
            return;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            _currentState = EntityStates.Walk;
            return;
        }

        if((_currentState == EntityStates.Idle || _currentState == EntityStates.Walk) && Input.GetKeyDown(KeyCode.Mouse1))
        {
            _currentState = EntityStates.RangeAttack;
            return;
        }

        if (!Input.anyKey)
        {
            _currentState = EntityStates.Idle;
            return;
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
