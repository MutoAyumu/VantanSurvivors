using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerativeAbility : IPassive
{
    PassiveDef _passiveId = PassiveDef.Regenerative;
    int _level = 1;

    float _setTime;
    Timer _timer = new Timer();

    public event System.Action _event;
    public PassiveDef PassiveId { get => _passiveId;}

    public void Levelup()
    {
        if (_level > 5) return;

        _setTime -= 0.5f;
        _timer.Setup(_setTime);
        _level++;
        Debug.Log($"<color=yellow>{this}</color> : レベルアップ{_level}");
    }

    public void Setup()
    {
        Debug.Log($"<color=yellow>{this}</color> : アビリティの追加");
        _setTime = 3f;
        _timer.Setup(_setTime);
    }

    public void Update()
    {
        if(_timer.RunTimer())
        {
            _event?.Invoke();
        }
    }
}
