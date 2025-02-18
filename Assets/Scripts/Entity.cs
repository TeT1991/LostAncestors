using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected EntityConfig _config;

    public TMPro.TextMeshProUGUI _textMeshPro;

    public List<StateConditions> Conditions { get; protected set; }
    protected EntityStates CurrentState;

    private SpriteRenderer _spriteRenderer;


    private void Awake()
    {
        Init();
    }

    protected virtual void Update()
    {
        SwitchState();
        ApplyStateActions();
    }

    protected virtual void Init()
    {
        LoadConfig();
        InitComponents();

        _spriteRenderer = GetComponent<SpriteRenderer>();
        Conditions = new List<StateConditions>
        {
        };
    }

    protected virtual void SwitchState() 
    {
        foreach (var condition in Conditions)
        {
            if (condition.CanChange(CurrentState))
            {
                CurrentState = condition.Type;
                Debug.Log(CurrentState);
                return;
            }
        }
    }
    protected virtual void ApplyStateActions() { }
    protected virtual void ApplyWalkStateActions() { }
    protected virtual void ApplyJumpStateActions() { }
    protected virtual void ApplyRangeAttackStateActions() { }
    protected virtual void ApplyPatrolingStateActions() { }
    protected virtual void LoadConfig() { }
    protected virtual void InitComponents() { }

    protected void FlipSprites(float direction)
    {
        bool canFlip = (direction <= 0);

        _spriteRenderer.flipX = canFlip;
    }
}
