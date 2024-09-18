using UnityEngine;
using UniRx;

public class DefenceAct : IAction
{
    public void Act(Units actUnit, float successRate)
    {
        if (!SuccessRateCalculator.CalculateSucess(successRate))
        {
            Debug.Log("Defence Action Fail");
            // AP 1ȹ��
            if (actUnit is Player)
            {
                Debug.Log("Action Point + 1");
            }
            actUnit.ActionFail();
            return;
        }

        Debug.Log("Do Defence");
        if (actUnit is Player)
        {
            Player player = actUnit as Player;
            player.ActionSuccessSubject.OnNext(Unit.Default);
        }
        actUnit.UseActionPoint();
        actUnit.Defence();
    }
}
