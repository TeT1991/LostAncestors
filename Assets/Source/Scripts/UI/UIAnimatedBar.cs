using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimatedBar : UIBar
{
    [SerializeField] private Image _image;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _animationTime;
    [SerializeField] private float _delay = 0;

    private float _currentValue;
    private float _passedAnimationTime;

    private Coroutine _coroutine;

    public override void Init(float currentValue)
    {
        _currentValue = currentValue;
        _image.fillAmount = _currentValue;
    }

    public override void ChangeValues(float value)
    {
        CurrentValue = Mathf.Clamp01(value);

        _passedAnimationTime = 0f;

        _coroutine = StartCoroutine(DelayAnimation());
    }

    private IEnumerator DelayAnimation()
    {
        yield return new WaitForSeconds(_delay);

        float start = _image.fillAmount;
        float time = 0;

        while (time < _animationTime)
        {
            time += Time.deltaTime;
            float passedTime = Mathf.Clamp01(time / _animationTime);

            float curved = _curve.Evaluate(passedTime);
            float step = Mathf.MoveTowards(start, CurrentValue, Mathf.Abs(CurrentValue - start) * curved);

            _image.fillAmount = step;

            yield return null;
        }

        _image.fillAmount = CurrentValue;
    }
}