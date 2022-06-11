using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpAbility : IPassive
{
    PassiveDef _passiveId = PassiveDef.SpeedUp;
    int _level = 1;
    public event System.Action _event;

    public PassiveDef PassiveId { get => _passiveId;}

    public void Levelup()
    {
        if (_level > 5) return;

        _level++;
        _event?.Invoke();

        Debug.Log($"<color=yellow>{this}</color> : レベルアップ{_level}");
    }

    public void Update()
    {

    }
    public void Setup()
    {
        _event?.Invoke();
        Debug.Log($"<color=yellow>{this}</color> : アビリティの追加");
    }
}
