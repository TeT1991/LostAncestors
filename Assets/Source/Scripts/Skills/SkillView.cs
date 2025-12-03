using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SkillView : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    public void Init()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ShowFX()
    {
        _spriteRenderer.enabled = true;
    }

    public void HideFX()
    {
        _spriteRenderer.enabled = false;
    }
}
