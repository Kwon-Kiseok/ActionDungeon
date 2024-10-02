using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private AttackAct _attackAct = new AttackAct();
    private DefenceAct _defenceAct = new DefenceAct();
    private DodgeAct _dodgeAct = new DodgeAct();

    public void DoAttackAction(Units actUnit, Units targetUnit, float successRate)
    {
        _attackAct.SetTargetUnit(targetUnit);
        float calculateSuccessRate = successRate + (0.15f * actUnit.ActionPoint);
        _attackAct.Act(actUnit, calculateSuccessRate);
    }

    public void DoDefenceAction(Units actUnit, float successRate)
    {
        float calculateSuccessRate = successRate + (0.15f * actUnit.ActionPoint);
        _defenceAct.Act(actUnit, calculateSuccessRate);
    }

    public void DoDodgeAction(Units actUnit, float successRate)
    {
        float calculateSuccessRate = successRate + (0.15f * actUnit.ActionPoint);
        _dodgeAct.Act(actUnit, calculateSuccessRate);
    }

    public void RandomAction(Units actUnit, Units targetUnit, float successRate)
    {
        int actionNumber = UnityEngine.Random.Range(0, 3);

        float calculateSuccessRate = successRate + (0.15f * actUnit.ActionPoint);

        switch (actionNumber)
        {
            case 0:
                DoAttackAction(actUnit, targetUnit, calculateSuccessRate);
            break;
            case 1:
                DoDefenceAction(actUnit, calculateSuccessRate);
            break;
            case 2:
                DoDodgeAction(actUnit, calculateSuccessRate);
            break;
        }
    }
}
