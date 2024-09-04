using UnityEngine;

public class FailState : IState
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
        // 해당 유닛의 액션 포인트를 여기서 올려주는 게 좋을 듯
        _unit.AddActionPoint(1);
    }

    public void ExitState()
    {
    }
}
