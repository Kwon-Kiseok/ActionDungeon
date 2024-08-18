using System;
using UnityEditor;
using UnityEngine;

public class AttackAct : IAction
{
    private ActionType _type = ActionType.ATTACK;

    private Units _targetUnit;

    public void Act(Units actUnit, float successRate)
    {
        if(!CalculateSucess(successRate))
        {
            Debug.Log("Attack Action Fail");
            // AP 1È¹µæ
            return; 
        }

        Debug.Log("Do Attack");
        actUnit.Attack(_targetUnit);
    }

    public void SetTargetUnit(Units targetUnit)
    {
        _targetUnit = targetUnit;
    }
    
    private bool CalculateSucess(float successRate)
    {
        float success = UnityEngine.Random.Range(0.0f, 1.0f);

        if(success >= (1.0f - successRate))
        {
            return true;
        }
        return false;
    }
} 
