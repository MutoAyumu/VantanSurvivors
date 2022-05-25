using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStatus : ScriptableObject
{
    [SerializeField] float _hp, _speed, _power;
    [SerializeField] string _animName;

    public float Hp { get => _hp; }
    public float Speed { get => _speed;}
    public float Power { get => _power;}
    public string AnimName { get => _animName;}
}
