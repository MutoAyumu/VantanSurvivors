using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//csv
public class GameData
{
    //GSSを読み込んで分割
    //各データとして保存
    static public List<AbilityTable> AbilitySelectTable = new List<AbilityTable>();

    public GameData(string[] _dataTable)
    {
        var type = int.Parse(_dataTable[0]);
        var id = int.Parse(_dataTable[1]);
        var name = _dataTable[2];
        var level = int.Parse(_dataTable[3]);
        var probability = int.Parse(_dataTable[4]);

        switch ((SelectType)type)
        {
            case SelectType.Skill:
                var s = new AbilityTable() { Type = SelectType.Skill, Id = id, Name = name, Level = level, Probability = probability };
                AbilitySelectTable.Add(s);
                break;

            case SelectType.Passive:
                var p = new AbilityTable() { Type = SelectType.Passive, Id = id, Name = name, Level = level, Probability = probability };
                AbilitySelectTable.Add(p);
                break;
        }

        Debug.Log($"ID {id} : Name {name} : Level {level} : Probability {probability}");
    }
}
public enum SelectType
{
    Skill = 1,
    Passive = 2,
}

[Serializable]
public class AbilityTable
{
    public SelectType Type;
    public int Id;
    public string Name;
    public int Level;
    public int Probability;
}
