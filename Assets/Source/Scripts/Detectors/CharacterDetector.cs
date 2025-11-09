using System;
using UnityEngine;

public class CharacterDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _layerToDetection;
    private float _detectionDistance;

    private Transform _lastDetectedObject = null;

    public event Action<Transform> OnDetected;
    public event Action OnNotDetected;

    private void Update()
    {
        SendRay();
    }

    public void Init(float detectionDistance)
    {
        _detectionDistance = detectionDistance;
    }

    private void SendRay()
    {
        Vector2 direction = transform.right;
        Vector2 origin = transform.position;

        Debug.DrawRay(origin, direction * _detectionDistance, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, _detectionDistance, _layerToDetection);

        if (hit.collider != null)
        {
            if (_lastDetectedObject != hit.collider.transform)
            {
                OnDetected?.Invoke(hit.collider.transform);
            }
            _lastDetectedObject = hit.collider.transform;
        }
        else
        {
            if (_lastDetectedObject != null)
            {
                OnNotDetected?.Invoke();
                _lastDetectedObject = null;
            }
        }
    }
}