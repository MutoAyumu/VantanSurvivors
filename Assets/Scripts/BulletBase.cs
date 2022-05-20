using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour, IObjectPool
{

    SpriteRenderer _sprite;

    private void OnEnable()
    {

    }
    private void OnBecameInvisible()
    {
        Destroy();
    }
    bool _isActive = false;
    public bool IsActive => _isActive;
    public void DisactiveForInstantiate()
    {
        _sprite.enabled = false;
        _isActive = false;
    }
    public void Create()
    {
        _sprite.enabled = true;
        _isActive = true;
    }
    public void Destroy()
    {
        _sprite.enabled = false;
        _isActive = false;
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
}
