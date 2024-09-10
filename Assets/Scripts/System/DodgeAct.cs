using UnityEngine;

public class DodgeAct : IAction
{
    public void Act(Units actUnit, float successRate)
    {
        if (!SuccessRateCalculator.CalculateSucess(successRate))
        {
            Debug.Log("Dodge Action Fail");
            // AP 1È¹µæ
            if (actUnit is Player)
            {
                Debug.Log("Action Point + 1");
            }
            actUnit.ActionFail();
            return;
        }

        Debug.Log("Do Dodge");
        actUnit.UseActionPoint();
        actUnit.Dodge();
    }
}
