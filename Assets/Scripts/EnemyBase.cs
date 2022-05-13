using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb = default;

    public Rigidbody2D Rb { get => _rb; }

    private void Start()
    {
        _rb.velocity = this.transform.up * Random.Range(3f,6f);
    }
}
