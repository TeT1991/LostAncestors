using UnityEngine;

public class JumpStateConditions : StateConditions
{
    private Jumper _jumper;

    public JumpStateConditions(Jumper jumper)
    {
        _jumper = jumper;
        _stateType = EntityStates.Jump;
        _allowedStates.Add(EntityStates.Idle);
        _allowedStates.Add(EntityStates.Walk);
    }

    public override bool CanChange(EntityStates currentState)
    {
        // ���������: ����� ������ + �������� �� ����� + ����������� ���������
        return HasAllowedState(currentState)
            && Input.GetKeyDown(KeyCode.Space)
            && _jumper.IsGrounded;
    }
}