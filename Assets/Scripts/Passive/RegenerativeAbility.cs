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
        Debug.Log($"<color=yellow>{this}</color> : ���x���A�b�v{_level}");
    }

    public void Setup()
    {
        Debug.Log($"<color=yellow>{this}</color> : �A�r���e�B�̒ǉ�");
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
