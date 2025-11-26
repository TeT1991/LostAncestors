using UnityEngine;
using UnityEngine.UI;

public class UIStaticBar : UIBar
{
    [SerializeField] private Image _image;

    public override void ChangeValues(float value)
    {
        base.ChangeValues(value);
        CurrentValue = value;
        UpdateBar();
    }

    private void UpdateBar()
    {
        _image.fillAmount = CurrentValue;
    }
}
