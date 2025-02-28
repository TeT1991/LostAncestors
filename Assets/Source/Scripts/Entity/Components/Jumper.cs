using UnityEngine;

public class Jumper : MonoBehaviour
{
    private float _jumpHeight; 
    private float _gravity; 
    private float _verticalVelocity; 
    private bool _isGrounded;

    public float VerticalSpeed => _verticalVelocity; 

    public void Init(float jumpHeight)
    {
        _gravity = -9.81f;
        _jumpHeight = jumpHeight;
        _isGrounded = true;
    }
    public void Jump()
    {
        float value = -2f;
        _verticalVelocity = Mathf.Sqrt(value * _gravity * _jumpHeight);
        _isGrounded = false;
    }
    public void UpdatePosition()
    {
        if (_isGrounded == false)
        {
            _verticalVelocity += _gravity * Time.deltaTime;
            transform.Translate(0, _verticalVelocity * Time.deltaTime, 0, Space.World);
        }
    }
}