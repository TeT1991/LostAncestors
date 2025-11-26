using UnityEngine;

public class UIBar : MonoBehaviour
{
    protected float MaxValue;
    protected float CurrentValue;

    public virtual void Init(float currentValue) 
    {
        CurrentValue = currentValue;
        ChangeValues(CurrentValue);
    }

    public void SetMaxValue(float value)
    {
        MaxValue = value;
    }

    public virtual void ChangeValues(float value) { }
}
