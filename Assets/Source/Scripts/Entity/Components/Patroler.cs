using UnityEngine;

public class Patroler : MonoBehaviour
{
    private float _xDirection;

    private EnemyCollideDetector _collideDetector;

    public void Init(EnemyCollideDetector collideDetector, float direction)
    {
        _xDirection = direction;
        _collideDetector = collideDetector;
        _collideDetector.WallCollided += ReverseDirection;
    }

    public void ReverseDirection()
    {
        int value = -1;
        _xDirection *= value;
    }
}
