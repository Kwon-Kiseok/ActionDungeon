using UnityEngine;

public static class DamageCalculator
{
    public static float HitDamage = 0;
    public static float HitDamageCalculate(Units attacker, Units defender)
    {
        // ���� ������ ����
        // ���� ������ = (������ ���ݷ� - �ǰ��� ���� / 2) / 2 +- (����)
        Units.statData attackerStatData = attacker.GetStatData();
        float coefficient_hitDamage = attackerStatData.atk * attackerStatData.luk * 10; // �ǰ� ������ ���
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
