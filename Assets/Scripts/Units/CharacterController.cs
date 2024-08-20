using UnityEngine;

public class CharacterController : MonoBehaviour
{
    
    private AttackAct _attackAct = new AttackAct();
    private DefenceAct _defenceAct = new DefenceAct();
    private DodgeAct _dodgeAct = new DodgeAct();


    public void DoAttackAction(Units actUnit, Units targetUnit, float successRate)
    {
        _attackAct.SetTargetUnit(targetUnit);
        _attackAct.Act(actUnit, successRate);
    }

    public void DoDefenceAction(Units actUnit, float successRate)
    {
        _defenceAct.Act(actUnit, successRate);
    }

    public void DoDodgeAction(Units actUnit, float successRate)
    {
        _dodgeAct.Act(actUnit, successRate);
    }
}
