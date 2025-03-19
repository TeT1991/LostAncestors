using UnityEngine;

public class Jumper : MonoBehaviour
{
    private float _jumpPower; 
    private bool _isGrounded;

    private Rigidbody2D _rigidbody;

    public float VerticalSpeed => _rigidbody.velocity.y;

    public void Init(float jumpPower, Rigidbody2D rigidbody)
    {
        _jumpPower = jumpPower;
        _isGrounded = true;
        _rigidbody = rigidbody;
    }
    public void Jump()
    {
        _rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
    }
}