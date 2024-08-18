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
}
