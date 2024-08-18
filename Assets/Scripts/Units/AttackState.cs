using UnityEngine;

public class AttackState : IState
{
    private Units _unit;
    private Units _targetUnit;

    public void ChangeUnit(Units unit)
    {
        _unit = unit;

        EnterState();
    }

    public void SetTargetUnit(Units targetUnit)
    {
        _targetUnit = targetUnit;
    }

    public void EnterState()
    {
    }

    public void ExitState()
    {
    }

    public void ExecuteState()
    {
    }
}
