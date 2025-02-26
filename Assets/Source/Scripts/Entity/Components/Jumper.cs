using UnityEngine;

public class Jumper : MonoBehaviour
{
    private bool _isGrounded;
    private float _jumpHeight;
    private Rigidbody2D _rigidBody2D;

    public bool IsGrounded => _isGrounded;
    public float VerticalSpeed => _rigidBody2D.velocity.y;

    public void Init(float jumpHeight, Rigidbody2D rigidbody2D)
    {
        _jumpHeight = jumpHeight;
        _rigidBody2D = rigidbody2D;
    }

    public void Jump()
    {
        _rigidBody2D.AddForce(Vector2.up * _jumpHeight, ForceMode2D.Impulse);
    }
}