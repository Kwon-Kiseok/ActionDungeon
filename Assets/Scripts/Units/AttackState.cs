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
        // �°����� ��Ȳ�̰ų� �׼� ������ ��쿡 ��� ���ֿ� Hit ������ ����
        if(_targetUnit.StateMachine.CurrentState is AttackState || _targetUnit.StateMachine.CurrentState is FailState)
        {
            Debug.Log(_targetUnit.unitName + " hit to " + _unit.unitName);
            _targetUnit.Hit(DamageCalculator.HitDamageCalculate(_unit, _targetUnit));
        }
        else if(_targetUnit.StateMachine.CurrentState is DefenceState)
        {
            Debug.Log(_targetUnit.unitName + " do defence");
            float defenceHitDamage = Mathf.Round(DamageCalculator.HitDamageCalculate(_unit, _targetUnit) * 0.3f);
            _targetUnit.Hit(defenceHitDamage);
        }
        else
        {
            Debug.Log(_targetUnit.unitName + " do avoid");
        }

        ExitState();
    }
}
