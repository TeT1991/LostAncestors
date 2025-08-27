using UnityEngine;

public class Rotater
{
    private readonly Transform _transform;
    private Quaternion _positiveRotation = Quaternion.Euler(Vector3.up);
    private Quaternion _negativeRotation = Quaternion.Euler(Vector3.up * 180f);
    public Rotater(Transform transform)
    {
        _transform = transform;
    }

    public void Rotate(int direction)
    {
        Quaternion rotation = direction >= 0 ? _positiveRotation : _negativeRotation;
        _transform.transform.rotation = rotation;
    }
}
