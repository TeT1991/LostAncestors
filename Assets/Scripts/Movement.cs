using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Jumper))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _horizontalMaxSpeed;

    private Jumper _jumper;

    private float _horizontalSpeed;
    private float _verticalSpeed;

    private void Start()
    {
        _jumper = GetComponent<Jumper>();
    }

    private void Update()
    {
        SetDirectionalSpeed();

        _verticalSpeed = _jumper.GetVertiaclaSpeed();
        Debug.Log(_verticalSpeed);

        transform.Translate(new Vector2(_horizontalSpeed * Time.deltaTime, _verticalSpeed * Time.deltaTime));
    }

    private void SetDirectionalSpeed()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            _horizontalSpeed = Input.GetAxis("Horizontal") * _horizontalMaxSpeed;
        }
        else
        {
            _horizontalSpeed = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumper.StartJump();
        }
    }
}
