using UnityEngine;
using System;
using System.Collections.Generic;

public class RandomDraftSystem
{
    // NORMAL = 0.6  RARE = 0.3 UNIQUE = 0.075  LEGENDARY = 0.025
    private EnhaceBonus.Rarity GetRandomRarity()
    {
        float value = UnityEngine.Random.Range(0f, 1.0f);

        if(value <= 0.025f)
        {
            return EnhaceBonus.Rarity.LEGENDARY;
        }
        else if(value > 0.025f && value <= 0.1f)
        {
            return EnhaceBonus.Rarity.UNIQUE;
        }
        else if(value > 0.1f && value <= 0.4f)
        {
            return EnhaceBonus.Rarity.RARE;
        }
        else
        {
            return EnhaceBonus.Rarity.NORMAL;
        }
    }

    public EnhaceBonus GetRandomBonus(List<EnhaceBonus> enhanceBonusDataList)
    {
        EnhaceBonus.Rarity randomRarity = GetRandomRarity();

        List<EnhaceBonus> rarityList = new List<EnhaceBonus>();

        foreach(var enhanceBonus in enhanceBonusDataList)
        {
            if(enhanceBonus.GetRarity() == randomRarity)
            {
                rarityList.Add(enhanceBonus);
            }
        }

        int randomIndex = UnityEngine.Random.Range(0, rarityList.Count);
        return rarityList[randomIndex];
    }
}
