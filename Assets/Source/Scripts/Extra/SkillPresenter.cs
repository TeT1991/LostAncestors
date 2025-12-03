using UnityEngine;

public class SkillPresenter : MonoBehaviour
{
    [SerializeField] private SkillUIView _uiSkill;
    [SerializeField] private SkillView _skillView;
    private Skill _skill;

    public void Init(Skill skill)
    {
        _skill = skill;
        _uiSkill.Init(_skill.Duration);
        _skillView.Init();

        _skill.ProgreesChanged += _uiSkill.SetProgressBarCurrentValue;

        _skill.Reloaded += UnblockSkillIcon;
        _skill.Used += StartSkillVizualisation;
        _skill.ReloadStarted += StopSkillVizualisation;
    }

    private void StartSkillVizualisation(float value)
    {
        BlockSkillIcon();
        _uiSkill.SetProgressBarMaxValue(value);
        _skillView.ShowFX();
    }

    private void StopSkillVizualisation(float value)
    {
        _skillView.HideFX();
        _uiSkill.SetProgressBarMaxValue(value);
    }

    private void BlockSkillIcon()
    {
        _uiSkill.Block();
    }

    private void UnblockSkillIcon()
    {
        _uiSkill.Unblock();
    }
}
