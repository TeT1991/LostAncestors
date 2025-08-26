using System;
using UnityEngine;

public class HoleDetector : MonoBehaviour
{
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private LayerMask _groundLayer;

    private readonly float _checkDistance = 1f;
    private bool _wasHoleDetectedLastFrame = false;

    public event Action OnHoleDetected;

    private void Update()
    {
        TryDetectHole();
    }

    public void TryDetectHole()
    {
        Vector3 checkPosition = _groundCheckPoint.position + transform.right * _checkDistance;
        RaycastHit2D hit = Physics2D.Raycast(checkPosition, Vector2.down, 2f, _groundLayer);

        bool isHoleDetectedCurrently = hit.collider == null;

        if (isHoleDetectedCurrently && _wasHoleDetectedLastFrame == false)
        {
            OnHoleDetected?.Invoke();
        }

        _wasHoleDetectedLastFrame = isHoleDetectedCurrently;
    }
}