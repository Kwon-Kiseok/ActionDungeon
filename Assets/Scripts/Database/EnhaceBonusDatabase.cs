using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class EnhaceBonusDatabase
{
    private const string ENHANCE_BONUS_DB = "EnhanceBonus_Table";
    private List<EnhaceBonus> _enhaceBonusDataList = new List<EnhaceBonus>();
    public List<EnhaceBonus> enhaceBonusDataList => _enhaceBonusDataList;

    public void Load()
    {
        if(_enhaceBonusDataList.Count != 0) return;

        List<Dictionary<string, object>> dataList = CSVReader.Read($"Table/{ENHANCE_BONUS_DB}");

        for(int i = 0; i < dataList.Count; i++)
        {
            int dataID = (int)dataList[i]["ID"];
            EnhaceBonus.Rarity rarity = (EnhaceBonus.Rarity)Enum.Parse(typeof(EnhaceBonus.Rarity), dataList[i]["Rarity"].ToString());
            List<string> bonusStatTypeList = ParseStatsType(dataList[i]["Stat"].ToString());
            List<float> bonusStatValueList = ParseStatsValue(dataList[i]["Value"].ToString());

            EnhaceBonus data = new EnhaceBonus(dataID, StatData(bonusStatTypeList, bonusStatValueList), rarity);

            _enhaceBonusDataList.Add(data);
        }
    }

    private List<string> ParseStatsType(string input)
    {
        string[] strings= input.Split(',');
        return strings.ToList<string>();
    }

    private List<float> ParseStatsValue(string input)
    {
        string[] strings = input.Split(',');
        List<float> values = new List<float>();
        foreach(var str in strings)
        {
            values.Add(float.Parse(str, System.Globalization.NumberStyles.Float));
        }
        return values;
    }

    private Units.statData StatData(List<string> strings, List<float> valuse)
    {
        Units.statData statData = new Units.statData();

        for(int i = 0; i < strings.Count; i++)
        {
            if(strings[i].Equals("HP"))
            {
                statData.hp = valuse[i];
            }
            else if(strings[i].Equals("ATK"))
            {
                statData.atk = valuse[i];
            }
            else if(strings[i].Equals("DEF"))
            {
                statData.def = valuse[i];
            }
            else if(strings[i].Equals("LUK"))
            {
                statData.luk = valuse[i];
            }

            statData.desc = strings[i] + " " + valuse[i];
        }

        return statData;
    }
}
