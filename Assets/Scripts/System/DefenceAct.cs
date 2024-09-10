using UnityEngine;

public class DefenceAct : IAction
{
    public void Act(Units actUnit, float successRate)
    {
        if (!SuccessRateCalculator.CalculateSucess(successRate))
        {
            Debug.Log("Defence Action Fail");
            // AP 1È¹µæ
            if (actUnit is Player)
            {
                Debug.Log("Action Point + 1");
            }
            actUnit.ActionFail();
            return;
        }

        Debug.Log("Do Defence");
        actUnit.UseActionPoint();
        actUnit.Defence();
    }
}
