using UnityEngine;

public static class DamageCalculator
{
    public static float HitDamage = 0;

    public static float coefficient_hitDamage = 50; // 피격 데미지 계수

    public static float HitDamageCalculate(Units attacker, Units defender)
    {
        // 최종 데미지 공식
        // 최종 데미지 = (공격자 공격력 - 피격자 방어력 / 2) / 2 +- (난수)

        float randHitDamage = UnityEngine.Random.Range((coefficient_hitDamage / 8), (coefficient_hitDamage / 16) + 1);

        int plusMinus = UnityEngine.Random.Range(1, 7);
        if(plusMinus <= 3)
        {
            randHitDamage *= -1;
        }

        HitDamage = (attacker.GetStatData().atk - (defender.GetStatData().def * 0.5f)) * 0.5f + randHitDamage;
        HitDamage = Mathf.Round(HitDamage);
        return HitDamage;
    }
}
