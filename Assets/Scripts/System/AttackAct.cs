using System;
using UnityEditor;
using UnityEngine;

public class AttackAct : IAction
{
    private ActionType _type = ActionType.ATTACK;

    private Units _targetUnit;

    public void Act(Units actUnit, float successRate)
    {
        if(!SuccessRateCalculator.CalculateSucess(successRate))
        {
            Debug.Log("Attack Action Fail");
            // AP 1ȹ��
            if(actUnit is Player)
            {
                Debug.Log("Action Point + 1");
            }
            actUnit.ActionFail();
            return; 
        }

        Debug.Log("Do Attack");
        actUnit.UseActionPoint();
        actUnit.Attack(_targetUnit);
    }

    public void SetTargetUnit(Units targetUnit)
    {
        _targetUnit = targetUnit;
    }
}
