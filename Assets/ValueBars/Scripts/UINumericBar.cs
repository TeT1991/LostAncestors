using TMPro;
using UnityEngine;

public class UINumericBar : UIBar
{
    [SerializeField] private TextMeshProUGUI _text;

    public override void ChangeValues(float value)
    {
        CurrentValue = value;
        string template = CurrentValue + "/" + MaxValue;
        UpdateBar(template);
    }

    private void UpdateBar(string text)
    {
        _text.text = text;
    }
}
