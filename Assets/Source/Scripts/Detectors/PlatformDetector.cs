using System;
using UnityEngine;

public class PlatformDetector : Detector
{
    private bool _isDetected = false;

    public event Action<bool> Collided;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isDetected = true; // Обновляем флаг
        Collided?.Invoke(true);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Проверяем, есть ли компонент GroundObstacle у объекта столкновения
        if (collision.gameObject.TryGetComponent(out GroundObstacle platform))
        {
            // Если столкновение обнаружено впервые, вызываем событие
            if (!_isDetected)
            {
                _isDetected = true; // Обновляем флаг
                Collided?.Invoke(true); // Уведомляем подписчиков о начале столкновения
            }
        }
        else
        {
            _isDetected = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out GroundObstacle platform))
        {
            Collided?.Invoke(false);
        }
    }
}