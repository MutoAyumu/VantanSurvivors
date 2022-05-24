using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour, IObjectPool
{
    SpriteRenderer _image = default;
    protected Rigidbody2D _rb = default;
    Collider2D _col = default;
    [SerializeField] protected float _speed = 1f;

    private void Start()
    {
        _image = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
    }
    private void OnEnable()
    {

    }
    bool _isActive = false;
    public bool IsActive => _isActive;
    public void DisactiveForInstantiate()
    {
        _image.enabled = false;
        _isActive = false;
        _rb.simulated = false;
        _col.enabled = false;
    }
    public void Create()
    {
        _image.enabled = true;
        _isActive = true;
        _rb.simulated = true;
        _col.enabled = true;
    }
    public void Create(EnemyStatus status)
    {

    }
    public void Destroy()
    {
        _image.enabled = false;
        _isActive = false;
        _rb.simulated = false;
        _col.enabled = false;
    }

    void Pause()
    {
        if (IsActive)
        {

        }
    }
    void Resume()
    {
        if (IsActive)
        {

        }
    }
    public virtual void Shoot(EnemyBase enemy)
    {

    }
}
