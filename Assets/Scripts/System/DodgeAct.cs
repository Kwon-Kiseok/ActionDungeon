using UnityEngine;

public class DodgeAct : IAction
{
    public void Act(Units actUnit, float successRate)
    {
        if (!SuccessRateCalculator.CalculateSucess(successRate))
        {
            Debug.Log("Dodge Action Fail");
            // AP 1ȹ��
            if (actUnit is Player)
            {
                Debug.Log("Action Point + 1");
            }

            return;
        }

        Debug.Log("Do Dodge");
        actUnit.Dodge();
    }
}
