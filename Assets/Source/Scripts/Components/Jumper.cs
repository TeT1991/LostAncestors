using UnityEngine;

public class Jumper : MonoBehaviour
{
    private float _jumpPower; 
    private bool _isGrounded;
    private float _jumpPowerModifier;

    private Rigidbody2D _rigidbody;

    public float VerticalSpeed => _rigidbody.velocity.y;

    public void Init(float jumpPower, Rigidbody2D rigidbody)
    {
        _jumpPower = jumpPower;
        _isGrounded = true;
        _rigidbody = rigidbody;
        _jumpPowerModifier = 1;
    }
    public void Jump()
    {
        _rigidbody.AddForce(_jumpPower * _jumpPowerModifier * Vector2.up, ForceMode2D.Impulse);
    }

    public void SetJumpPower(float jumpPower)
    {
        _jumpPower = jumpPower;
    }

    public void ResetModifier()
    {
        _jumpPowerModifier = 0;
    }

    public void SetDeafaultModifier()
    {
        _jumpPowerModifier = 1;
    }
}