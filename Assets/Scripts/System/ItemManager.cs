using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    static ItemManager _instance = new ItemManager();

    //アイテム関係のプールを並べる
    ObjectPool<ExpPoint> _expPoints = new ObjectPool<ExpPoint>();

    public static ItemManager Instance { get => _instance;}

    private ItemManager() { }

    public void SetUp()
    {
        var exp = Resources.Load<ExpPoint>("ExpPoint");
        var expRoot = new GameObject("ExpRoot").transform;
        _expPoints.SetBaseObj(exp, expRoot);
        _expPoints.SetCapacity(100);
    }
    public void SetExp(Transform transform)// ここあとで直すかも
    {
        var e = _expPoints.Instantiate();
        e.transform.position = transform.position;
    }
}
