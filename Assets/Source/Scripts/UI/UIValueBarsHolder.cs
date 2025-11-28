using TMPro;
using UnityEngine;

public class UIValueBarsHolder : MonoBehaviour
{
    [SerializeField] private UIBar _numericBar;
    [SerializeField] private UIBar _dynamicBar;

    private float _currentCountValue;
    private float _maxCountValue;

    public void Init(float currentCountValue, float maxCountValue)
    {
        _currentCountValue = currentCountValue;
        _maxCountValue = maxCountValue;
        _numericBar.Init(_currentCountValue);
        _numericBar.SetMaxValue(_maxCountValue);
        _dynamicBar.Init(CalculateFillnesPercent());
    }

    public void SetMaxValue(float value)
    {
        _numericBar.SetMaxValue(value);
        _dynamicBar.SetMaxValue(value);
    }

    public void ChangeValue(float value)
    {
        float newValue = Mathf.Clamp(value, 0, _maxCountValue);

        if (newValue == _currentCountValue)
        {
            return;
        }


        _currentCountValue = newValue;
        _numericBar.ChangeValues(_currentCountValue);
        _dynamicBar.ChangeValues(CalculateFillnesPercent());
    }

    private float CalculateFillnesPercent()
    {
        return Mathf.Clamp(_currentCountValue / _maxCountValue, 0f, 1f);
    }
}
