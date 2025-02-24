using UnityEngine;

public class Jumper : MonoBehaviour
{
    private bool _isGrounded;
    private float _jumpHeight;
    private Rigidbody2D _rb;

    public bool IsGrounded => _isGrounded;

    public void Init(float jumpHeight)
    {
        _jumpHeight = jumpHeight;
        _rb = GetComponent<Rigidbody2D>(); // �������� Rigidbody2D
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            // ��������� ������� �����
            _rb.AddForce(Vector2.up * _jumpHeight, ForceMode2D.Impulse);
            _isGrounded = false; // �������� ������ �� �� �����
        }
    }

    // ��������� ������ ����� ����� ��������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            _isGrounded = true; // ��������, ��� �������� �� �����
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            _isGrounded = false; // ��������, ��� �������� �� �����
        }
    }
}