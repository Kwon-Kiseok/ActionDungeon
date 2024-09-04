using UnityEngine;

public class DefenceState : IState
{
    private Units _unit;

    public void ChangeUnit(Units unit)
    {
        _unit = unit;

        EnterState();
    }

    public void EnterState()
    {
    }

    public void ExecuteState()
    {
    }

    public void ExitState()
    {
    }
}
