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
        AllowJump();
    }

    public void Jump(float jumpPower)
    {
        if(_canJump)
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            DenyJump();
        }
    }

    public void AllowJump()
    {
        _canJump = true;
    }

    public void DenyJump()
    {
        _canJump = false;
    }
}
