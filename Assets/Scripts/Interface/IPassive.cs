using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPassive
{
    PassiveDef PassiveId { get; }
    public event System.Action _event;

    void Setup();
    void Update();
    void Levelup();
}
public enum PassiveDef
{
    Invalid = 0,
    SpeedUp = 1,
    HitpointUp = 2,
    Regenerative = 3,
}