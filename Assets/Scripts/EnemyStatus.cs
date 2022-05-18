using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStatus : ScriptableObject
{
    [SerializeField] float _hp, _speed, _power;
    [SerializeField] Sprite _sprite;

    public float Hp { get => _hp; }
    public float Speed { get => _speed;}
    public float Power { get => _power;}
    public Sprite Sprite { get => _sprite;}
}
