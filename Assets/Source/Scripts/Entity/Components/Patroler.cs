using UnityEngine;

public class Patroler : MonoBehaviour
{
    private float _xDirection;

    private CollideDetector _collideDetector;

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
