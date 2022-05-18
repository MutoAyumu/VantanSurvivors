using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IObjectPool
{
    [SerializeField] EnemyStatus _status = default;
    [SerializeField] float _radius = 0.5f;

    float _speed;
    Transform _player = default;
    SpriteRenderer _sprite;

    public float Radius { get => _radius;}

    private void Awake()
    {
        _sprite = this.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _speed = _status.Speed;
        _player = GameManager.Player.transform;
    }
    private void Update()
    {
        if (!IsActive) return;

        Vector3 dir = _player.position - this.transform.position;
        dir.Normalize();

        transform.position += dir * _speed * Time.deltaTime;
    }

    /// <summary>
    /// “–‚½‚è”»’è‚Ì”ÍˆÍ‚ð•\Ž¦
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, _radius);
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
}
