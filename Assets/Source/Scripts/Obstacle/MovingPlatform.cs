using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : GroundObstacle, IInteractable
{
    [SerializeField] private List<Transform> _stops;

    private float _speed = 3;
    private int _lastStop;
    private int _nextStop;
    bool _isMoving = false;
    private int _direction = 0;

    private void Awake()
    {
        SetStartStop();
    }

    private void Update()
    {
        TryMove();
        TryStopMove();
    }

    public void SetDirection(int value)
    {
        _direction = value;
    }

    private void TryMove()
    {
        if (_direction != 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, _stops[_nextStop].transform.position, _speed * Time.deltaTime);
        }
    }

    public void SetNextStop(int index)
    {
        _direction = index;

        if (_isMoving == false)
        {
            if (_lastStop + _direction <= _stops.Count - 1 && _lastStop + _direction >= 0)
            {
                _nextStop = _lastStop + index;
                _isMoving = true;
            }
            else
            {
                _direction = 0;
            }
        }
        else
        {
            _nextStop += index;
        }
    }

    private void SetStartStop()
    {
        for (int i = 0; i < _stops.Count; i++)
        {
            if (Vector2.Distance(transform.position, _stops[i].transform.position) < 0.1f)
            {
                _lastStop = i;
            }
        }
    }

    private void TryStopMove()
    {
        if (Vector2.Distance(transform.position, _stops[_nextStop].position) < 0.1f)
        {
            _direction = 0;
            _lastStop = _nextStop;
            _isMoving = false;
        }
    }

    public void Interact()
    {

    }

    public void ShowMessage()
    {

    }

    public void HideMessage()
    {

    }
}
