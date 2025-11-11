using UnityEngine;

public class Rotater
{
    private readonly Transform _transform;
    private Quaternion _positiveRotation = Quaternion.Euler(Vector3.up);
    private Quaternion _negativeRotation = Quaternion.Euler(Vector3.up * 180f);
    private Quaternion _currentRotation;

    public Rotater(Transform transform)
    {
        _transform = transform;
    }

    public void Rotate(int direction)
    {
        Quaternion rotation = direction >= 0 ? _positiveRotation : _negativeRotation;

        if (_currentRotation != rotation)
        {
            _transform.transform.rotation = rotation;
            _currentRotation = rotation;
        }
    }
}
