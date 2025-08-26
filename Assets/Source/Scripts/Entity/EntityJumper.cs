using System.Diagnostics;
using UnityEngine;

public class EntityJumper
{
    private readonly Rigidbody2D _rigidbody;

    private bool _canJump;

    public bool CanJump => _canJump;

    public EntityJumper(Rigidbody2D rigidbody)
    {
        _rigidbody = rigidbody;
        TryAllowJump(true);
    }

    public void Jump(float jumpPower)
    {
        if(_canJump)
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            TryAllowJump(false);
        }
    }

    public void TryAllowJump(bool value)
    {
        _canJump = value;
    }
}
