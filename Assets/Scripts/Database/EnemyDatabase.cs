using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class EnemyDatabase
{
    private const string ENEMY_DATA_DB = "EnemyDataList_Table";

    private Dictionary<int, Units.statData> _enemyStatDataDictionary = new Dictionary<int, Units.statData>();

    public void Load()
    {
        if(_enemyStatDataDictionary.Count != 0) return;

        List<Dictionary<string, object>> dataList = CSVReader.Read($"Table/{ENEMY_DATA_DB}");

        for(int i = 0; i < dataList.Count; i++)
        {
            int dataID = (int)dataList[i]["ID"];
            string dName = dataList[i]["Name"].ToString();
            float dHp = float.Parse(dataList[i]["HP"].ToString(), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
            float dAtk = float.Parse(dataList[i]["ATK"].ToString(), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
            float dDef = float.Parse(dataList[i]["DEF"].ToString(), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
            float dLuk = float.Parse(dataList[i]["LUK"].ToString(), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
        
            Units.statData enemyData = new Units.statData(dName, dHp, dAtk, dDef, dLuk);
            _enemyStatDataDictionary.Add(dataID, enemyData);
        }
    }

    public Units.statData GetStatData(int dataID)
    {
        return _enemyStatDataDictionary[dataID];
    }
}
