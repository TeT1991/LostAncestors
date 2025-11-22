using TMPro;
using UnityEngine;

public class UIValueBarHolder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private UIBar _foregroundBar;
    [SerializeField] private UIBar _backgroundBar;

    [SerializeField] private Color _foregroundBarColor;
    [SerializeField] private float _foregroundBarDelay;
    [SerializeField] private float _foregroundBarAnimationTime;

    [SerializeField] private Color _backgroundBarDecreaseColor;
    [SerializeField] private Color _backgroundBarIncreaseColor;
    [SerializeField] private float _backgroundBarDelay;
    [SerializeField] private float _backgroundBarAnimationTime;

    float _currentCountValue = 50;
    float _maxCountValue = 100;

    private void Awake()
    {
        _foregroundBar.Init(CalculateFillnesPercent());
        _backgroundBar.Init(CalculateFillnesPercent());
    }

    public void DecreaseValue(float value)
    {
        float newValue = Mathf.Clamp(_currentCountValue - value, 0, _maxCountValue);

        if (newValue == _currentCountValue)
        {
            return;
        }
        _currentCountValue = newValue;

        _foregroundBar.ChageValues(CalculateFillnesPercent(), _foregroundBarColor, _foregroundBarDelay, _foregroundBarAnimationTime);
        _backgroundBar.ChageValues(CalculateFillnesPercent(), _backgroundBarDecreaseColor, _backgroundBarDelay, _backgroundBarAnimationTime);
    }

    public void IncreaseValue(float value)
    { 
        float newValue = Mathf.Clamp(_currentCountValue + value, 0, _maxCountValue);

        if(newValue == _currentCountValue)
        {
            return;
        }
        _currentCountValue = newValue;

        _foregroundBar.ChageValues(CalculateFillnesPercent(), _foregroundBarColor, _backgroundBarDelay, _backgroundBarAnimationTime);
        _backgroundBar.ChageValues(CalculateFillnesPercent(), _backgroundBarIncreaseColor, _foregroundBarDelay, _foregroundBarAnimationTime);
    }

    private float CalculateFillnesPercent()
    {
        return Mathf.Clamp(_currentCountValue / _maxCountValue, 0f, 1f);
    }
}
