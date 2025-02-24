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
        _rb = GetComponent<Rigidbody2D>(); // Получаем Rigidbody2D
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            // Применяем импульс вверх
            _rb.AddForce(Vector2.up * _jumpHeight, ForceMode2D.Impulse);
            _isGrounded = false; // Персонаж больше не на земле
        }
    }

    // Обновляем статус земли через коллизии
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            _isGrounded = true; // Сообщаем, что персонаж на земле
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            _isGrounded = false; // Сообщаем, что персонаж на земле
        }
    }
}