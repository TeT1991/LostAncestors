using TMPro;
using UnityEngine;

public class CharacterStatesHandle : MonoBehaviour
{
    public TextMeshProUGUI _text;

    [SerializeField] private CharacterSwitcher _characterSwitcher;
    [SerializeField] private CharacterCollideDetector _characterCollideDetector;

    private Character _character;

    private bool _isWalking = false;
    private bool _isJumping = false;
    private bool _isAttacking = false;
    private bool _isClimbing = false;
    private bool _isIdle = true;

    private void Update()
    {
        SetStatus();

        PerformWalkingAction();
        PerformJumpAction();
        PerformAttackAction();
        PerformClimbingAction();
        PerformIdleAction();
    }

    public void Init(Character character)
    {
        ResetBools();

        _character = character;
    }

    private void ResetBools()
    {
        _isWalking = false;
        _isJumping = false;
        _isAttacking = false;
        _isClimbing = false;
    }

    private void SetStatus()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (_isWalking == false && _isAttacking == false && _isJumping == false && _isClimbing == false)
            {
                if (_characterCollideDetector.IsGroundCollided)
                {
                    ResetBools();
                    _isWalking = true;

                    return;
                }
            }
        }
        else
        {
            _isWalking = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isAttacking == false && _isJumping == false && _isClimbing == false)
            {
                ResetBools();

                _character.Jumper.SetDeafaultModifier();
                _isJumping = true;

                return;
            }
        }
        else
        {
            if (_characterCollideDetector.IsGroundCollided)
            {
                _isJumping = false;
            }
        }

        if (_isAttacking == false && _isJumping == false && _isClimbing == false)
        {
            Projectile projectile;
            Transform launchPoint;

            if (Input.GetMouseButtonDown(0))
            {
                projectile = _character.MeleeProjectile;
                launchPoint = _character.ProjectileMeleeLaunchPoint;
                _character.Attacker.SetProjectile(projectile, launchPoint);
                ResetBools();
                _isAttacking = true;
                return;
            }

            else if (Input.GetMouseButtonDown(1))
            {
                projectile = _character.RangeProjectile;
                launchPoint = _character.ProjectileRangeLaunchPoint;
                _character.Attacker.SetProjectile(projectile, launchPoint);
                ResetBools();
                _isAttacking = true;
                return;
            }
            else
            {
                _isAttacking = false;
            }
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            if (_isAttacking == false)
            {
                if (_characterCollideDetector.IsLadderColided)
                {
                    ResetBools();
                    _isClimbing = true;
                    return;
                }
                else
                {
                    _isClimbing = false;
                }
            }
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space))
        {
            if (_isClimbing)
            {
                _character.Jumper.ResetModifier();
                _isClimbing = false;
                _isJumping = true;

                Debug.Log("Walking " + _isWalking);
                Debug.Log("Jumping " + _isJumping);
                Debug.Log("Attacking " + _isAttacking);
                Debug.Log("Climbing " + _isClimbing);
            }
        }

        if (_isWalking == false && _isAttacking == false && _isClimbing == false && _isJumping == false)
        {
            _isIdle = true;
        }
        else
        {
            _isIdle = false;
        }
    }

    private void PerformWalkingAction()
    {
        if (_isWalking)
        {
            _character.DirectionSwitcher.SetDirection(Input.GetAxis("Horizontal"));
            _character.Mover.MoveHorizontal(_character.GroundSpeed * _character.DirectionSwitcher.Direction);
            _character.AnimationSwitcher.TrySetAnimation("Walk", true);
        }
    }

    private void PerformJumpAction()
    {
        if (_isJumping)
        {
            _character.Jumper.Jump();

            string animationName = _character.Jumper.VerticalSpeed >= 0 ? "Jump_up" : "Jump_down";

            _character.AnimationSwitcher.TrySetAnimation(animationName, true);
        }
    }

    private void PerformAttackAction()
    {
        if (_isAttacking)
        {
            _character.AnimationSwitcher.TrySetAnimation("RangeAttack", false);
            _character.Attacker.ApplyAttack(_character.DirectionSwitcher.Direction);
            _isAttacking = false;
        }
    }

    private void PerformClimbingAction()
    {
        if (_isClimbing)
        {
            _character.RigidBody.gravityScale = 0;
            _character.Mover.MoveVecrtiacal(3 * Input.GetAxis("Vertical"));
        }
        else
        {
            _character.RigidBody.gravityScale = 1;
        }
    }

    private void PerformIdleAction()
    {
        if (_isIdle)
        {
            _character.AnimationSwitcher.TrySetAnimation("Idle", true);
        }
    }
}
