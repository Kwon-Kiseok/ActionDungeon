using UnityEngine;

public static class DamageCalculator
{
    public static float HitDamage = 0;
    public static float HitDamageCalculate(Units attacker, Units defender)
    {
        // 최종 데미지 공식
        // 최종 데미지 = (공격자 공격력 - 피격자 방어력 / 2) / 2 +- (난수)
        Units.statData attackerStatData = attacker.GetStatData();
        float coefficient_hitDamage = attackerStatData.atk * attackerStatData.luk * 10; // 피격 데미지 계수
        float randHitDamage = UnityEngine.Random.Range((coefficient_hitDamage / 8), (coefficient_hitDamage / 16) + 1);

        int plusMinus = UnityEngine.Random.Range(1, 7);
        if(plusMinus <= 3)
        {
            randHitDamage *= -1;
        }

        HitDamage = (attackerStatData.atk - (defender.GetStatData().def * 0.5f)) * 0.5f + randHitDamage;
        if(HitDamage < 0)
        {
            HitDamage = 0;
        }
        HitDamage = Mathf.Round(HitDamage);
        return HitDamage;
    }
}
