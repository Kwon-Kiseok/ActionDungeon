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

    public void RandomAction(Units actUnit, Units targetUnit, float successRate)
    {
        int actionNumber = UnityEngine.Random.Range(0, 3);

        switch(actionNumber)
        {
            case 0:
                DoAttackAction(actUnit, targetUnit, successRate);
            break;
            case 1:
                DoDefenceAction(actUnit, successRate);
            break;
            case 2:
                DoDodgeAction(actUnit, successRate);
            break;
        }
    }
}
