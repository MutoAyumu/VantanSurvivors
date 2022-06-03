using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム関係の基底クラスs
/// </summary>
public class ItemBase : MonoBehaviour, IObjectPool
{
    SpriteRenderer _sprite;
    Collider2D _collider;
    bool _isActive;
    public bool IsActive => _isActive;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //消す
            Destroy();
            AddProcess();
        }
    }

    /// <summary>
    /// 派生側で追加処理をしたいときに使う
    /// </summary>
    protected virtual void AddProcess()
    {

    }

    public void DisactiveForInstantiate()
    {
        _isActive = false;
        _sprite.enabled = false;
        _collider.enabled = false;
    }

    public void Create()
    {
        _isActive = true;
        _sprite.enabled = true;
        _collider.enabled = true;
    }

    public void Create(EnemyStatus status)
    {
    }

    public void Destroy()
    {
        _isActive = false;
        _sprite.enabled = false;
        _collider.enabled = false;
    }
}
