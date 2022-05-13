using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb = default;
    [SerializeField] float _speed = 5f;

    public Rigidbody2D Rb { get => _rb;}


    private void OnEnable()
    {
        _rb.velocity = this.transform.up * _speed;
    }
    private void OnBecameInvisible()
    {
        _rb.simulated = false;
        this.gameObject.SetActive(false);
    }
}
