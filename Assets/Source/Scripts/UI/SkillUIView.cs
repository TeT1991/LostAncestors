using UnityEngine;
using UnityEngine.UI;

public class SkillUIView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Color _blockedColor;
    [SerializeField] private UIValueBarsHolder _progressBar;

    public void Init(float value)
    {
        _progressBar.Init(value, value);
    }

    public void Block()
    {
        _image.color = _blockedColor;
    }

    public void Unblock()
    {
        _image.color = Color.white;
    }

    public void SetProgressBarCurrentValue(float value)
    {
        _progressBar.ChangeValue(value);
    }

    public void SetProgressBarMaxValue(float value)
    {
        _progressBar.SetMaxValue(value);
    }
}
