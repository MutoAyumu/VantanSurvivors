using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LaserSetup : ScriptableObject
{
    [SerializeField] int _reflectiveCount;
    [SerializeField] float _lineAnimationSpeed, _changeDistance;
    [SerializeField] LayerMask _mask;
    [SerializeField] Material _material;

    public int ReflectiveCount { get => _reflectiveCount;}
    public float LineAnimationSpeed { get => _lineAnimationSpeed;}
    public float ChangeDistance { get => _changeDistance;}
    public LayerMask Mask { get => _mask;}
    public Material Material { get => _material;}
}
