using System;

public class EnhaceBonus
{
    public enum Rarity
    {
        NORMAL,
        RARE,
        UNIQUE,
        LEGENDARY
    }

    private Units.statData _enhanceStatData;
    private Rarity _rarity;

    public EnhaceBonus(Units.statData enhanceStatData, Rarity rarity)
    {
        _enhanceStatData = enhanceStatData;
        _rarity = rarity;
    }
}
