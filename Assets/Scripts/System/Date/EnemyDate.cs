using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDate
{
    static public List<Param> EnemyParamList = new List<Param>();

    public EnemyDate(string[] _date)
    {
        new Param()
        {
            Name = _date[0],
            Hp = int.Parse(_date[1]),
            Speed = int.Parse(_date[2]),
            Power = int.Parse(_date[3]),
        };
    }
}
[System.Serializable]
public class Param
{
    public string Name;
    public float Hp;
    public float Speed;
    public float Power;
}