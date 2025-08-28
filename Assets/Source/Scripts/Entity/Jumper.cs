using UnityEngine;

public class Jumper
{
    private readonly Rigidbody2D _rigidbody;
    private readonly float _jumpPower;


    public Jumper(Rigidbody2D rigidbody, float jumpPower)
    {
        _rigidbody = rigidbody;
        _jumpPower = jumpPower;
    }

    public void Jump()
    {
        _rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
    }
}
