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
        // 맞공격인 상황이거나 액션 실패인 경우에 상대 유닛에 Hit 판정을 내림
        if(_targetUnit.StateMachine.CurrentState is AttackState || _targetUnit.StateMachine.CurrentState is FailState)
        {
            Debug.Log(_targetUnit.unitName + " hit to " + _unit.unitName);
            _targetUnit.Hit(DamageCalculator.HitDamageCalculate(_unit, _targetUnit));
        }
        else if(_targetUnit.StateMachine.CurrentState is DefenceState)
        {
            Debug.Log(_targetUnit.unitName + " do defence");
        }
        else
        {
            Debug.Log(_targetUnit.unitName + " do avoid");
        }

        ExitState();
    }
}
