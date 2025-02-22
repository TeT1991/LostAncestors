using UnityEngine;

public class Jumper : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private float _jumpForce;
    private bool _isGrounded;

    public bool IsGrounded => _isGrounded;
    public float CurrentVerticalSpeed => _rigidBody.velocity.y;
    public void Init(Rigidbody2D rigidBody, float jumpForce)
    {
        _rigidBody = rigidBody;
        _jumpForce = jumpForce;
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _isGrounded = false;
            _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    public void SetStatus(bool value)
    {
        _isGrounded = value;
    }
}
