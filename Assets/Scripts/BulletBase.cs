using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour, IObjectPool
{
    SpriteRenderer _image = default;
    Collider2D _col = default;
    [SerializeField] protected float _speed = 1f;
    [SerializeField] float _destroyLimit = 5f;

    protected Timer _timer = new Timer();

    private void Update()
    {
        if (!IsActive) return;

        OnUpdate();

        if(_timer.RunTimer())
        {
            Destroy();
        }
    }
    private void OnEnable()
    {

    }

    bool _isActive = false;
    public bool IsActive => _isActive;
    public void DisactiveForInstantiate()
    {
        _image = GetComponent<SpriteRenderer>();
        _col = GetComponent<Collider2D>();

        _image.enabled = false;
        _isActive = false;
        _col.enabled = false;
    }
    public void Create()
    {
        _image.enabled = true;
        _isActive = true;
        _col.enabled = true;

        _timer.Setup(_destroyLimit);
    }
    public void Create(EnemyStatus status)
    {

    }
    public void Destroy()
    {
        _image.enabled = false;
        _isActive = false;
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
    protected virtual void OnUpdate()
    {

    }
}
