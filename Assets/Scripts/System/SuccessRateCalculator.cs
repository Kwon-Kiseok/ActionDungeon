public static class SuccessRateCalculator
{
   public static bool CalculateSucess(float successRate)
    {
        float success = UnityEngine.Random.Range(0.0f, 1.0f);

        if (success >= (1.0f - successRate))
        {
            return true;
        }
        return false;
    }
}
