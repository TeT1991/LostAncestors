using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIBar : MonoBehaviour
{
    private float _delay;
    private float _animationTime;
    private bool _isAnimating = false;
    private float _currentValue;
    private float _maxValue;
    private float _step;
    private Image _image;
    private Coroutine _coroutine;

    private void Update()
    {
        if (_isAnimating)
        {
            SetFilness();
        }
    }

    public void Init(float currentValue)
    {
        _image = GetComponent<Image>();
        _currentValue = currentValue;
        _image.fillAmount = _currentValue;
    }

    public void SetDelay(float delay)
    {
        _delay = delay;
    }

    public void SetAnimationTime(float animationTime)
    {
        _animationTime = animationTime;
    }

    public void SetFilness()
    {
        _image.fillAmount = Mathf.Clamp01(Mathf.MoveTowards(_image.fillAmount, _currentValue, _step * Time.deltaTime));

        if (_image.fillAmount == _currentValue)
        {
            _isAnimating = false;

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
    }

    public void ChageValues(float value, Color color, float delay, float animationTime)
    {
        _currentValue = value;
        _image.color = color;
        _delay = delay;
        _animationTime = animationTime;

        float distance = CalculateDistance(_image.fillAmount, _currentValue);
        _step = CalculateStep(distance, _animationTime);

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(DelayAnimation());
    }

    private IEnumerator DelayAnimation()
    {
        WaitForSeconds waitForSeconds = new(_delay);

        yield return waitForSeconds;

        _isAnimating = true;
    }

    private float CalculateDistance(float current, float target)
    {
        return Mathf.Abs(current - target);
    }

    private float CalculateStep(float distance, float animationTime)
    {
        return distance / animationTime;
    }
}


