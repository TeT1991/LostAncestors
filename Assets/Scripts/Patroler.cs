using UnityEngine;

public class Patroler : MonoBehaviour
{
    public float _xDirection;

    private Mover _mover;

    private CollideDetector _collideDetector;

    public float XDirection => _xDirection;

    public void Init(CollideDetector collideDetector, float direction)
    {
        _xDirection = direction;
        _collideDetector = collideDetector;
        _collideDetector.ObstacleCollided += ReverseDirection;
    }

    public void ReverseDirection()
    {
        int value = -1;
        _xDirection *= value;
    }
}
