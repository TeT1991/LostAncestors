using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _timeToPeak = 1f;
    [SerializeField] private float _gravity = -9.81f;

    private float _currentTime = 0f;
    private bool _isJumping = false;
    private bool _isFalling = true;
    private float _currentSpeed = 0f;

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && !_isJumping && !_isFalling)
        //{
        //    StartJump();
        //}

        //if (_isJumping || _isFalling)
        //{
        //    UpdateMovement();
        //}
    }

    public void StartJump()
    {
        _isJumping = true;
        _currentTime = 0f;
        _currentSpeed = _maxSpeed;
    }

    void UpdateMovement()
    {
        if (_isJumping)
        {
            _currentTime += Time.deltaTime;

            _currentSpeed = _maxSpeed * (1 - _currentTime / _timeToPeak);

            if (_currentTime >= _timeToPeak)
            {
                _isJumping = false;
                _isFalling = true;
                _currentSpeed = 0f;
            }
            else
            {
                transform.position += Vector3.up * _currentSpeed * Time.deltaTime;
            }
        }
        else if (_isJumping == false)
        {
            _isJumping = true;
            _currentTime = 0f;
            _currentSpeed = _maxSpeed;
        }

        if (_isFalling)
        {
            _currentSpeed += _gravity * Time.deltaTime;

            if (_currentSpeed < -_maxSpeed)
            {
                _currentSpeed = -_maxSpeed;
            }

            transform.position += Vector3.up * _currentSpeed * Time.deltaTime;
        }
    }

    public float GetVertiaclaSpeed()
    {
        if (_isJumping == false)
        {
            _isJumping = true;
            _currentTime = 0f;
            _currentSpeed = _maxSpeed;
        }

        if (_isJumping)
        {
            _currentTime += Time.deltaTime;

            _currentSpeed = _maxSpeed * (1 - _currentTime / _timeToPeak);

            if (_currentTime >= _timeToPeak)
            {
                _isJumping = false;
                _isFalling = true;
                _currentSpeed = 0f;
            }
            else
            {
                _currentSpeed = _currentSpeed * Time.deltaTime;
            }
        }

        if (_isFalling)
        {
            _currentSpeed += _gravity * Time.deltaTime;

            if (_currentSpeed < -_maxSpeed)
            {
                _currentSpeed = -_maxSpeed;
            }

            _currentSpeed *= Time.deltaTime;
        }

        return _currentSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isFalling = false;
        _isJumping = false;
        Debug.Log("Collide");
        _currentSpeed = 0f;
    }
}
