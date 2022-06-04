using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 経験値用クラス
/// </summary>
public class ExpPoint : ItemBase
{
    [SerializeField] int _exp = 1;

    public int Exp { get => _exp;}

    public event System.Action<int> OnGetEvent;

    protected override void OnAwake()
    {
        PlayerManager.Instance.SetExpListener(this);
    }
    protected override void AddProcess()
    {
        //ここでプレイヤーの経験値を追加
        OnGetEvent?.Invoke(_exp);
    }
}
