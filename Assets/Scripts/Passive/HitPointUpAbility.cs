using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointUpAbility : IPassive
{
    PassiveDef _passiveId = PassiveDef.HitpointUp;
    int _level = 1;

    public event System.Action _event;
    public PassiveDef PassiveId { get => _passiveId;}

    public void Setup()
    {
        _event?.Invoke();
        Debug.Log($"<color=yellow>{this}</color> : �A�r���e�B�̒ǉ�");
    }
    public void Update()
    {

    }
    public void Levelup()
    {
        if (_level > 5) return;

        _level++;
        _event?.Invoke();

        Debug.Log($"<color=yellow>{this}</color> : ���x���A�b�v{_level}");
    }
}
