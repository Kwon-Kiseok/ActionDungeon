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

    private int _id;
    private Units.statData _enhanceStatData;
    private Rarity _rarity;

    public EnhaceBonus(int ID, Units.statData enhanceStatData, Rarity rarity)
    {
        _id = ID;
        _enhanceStatData = enhanceStatData;
        _rarity = rarity;
    }

    public int GetID()
    {
        return _id;
    }

    public Units.statData GetStatData()
    {
        return _enhanceStatData;
    }

    public string GetStatName()
    {
        return _enhanceStatData.desc;
    }

    public Rarity GetRarity()
    {
        return _rarity;
    }
}
