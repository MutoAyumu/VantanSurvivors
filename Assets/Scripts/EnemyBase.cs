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
    Rigidbody2D _rb;
    GameManager _gameManager;
    bool _isPause;

    public float Radius { get => _radius;}

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _sprite = this.GetComponent<SpriteRenderer>();
        _rb = this.GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        _gameManager.OnPause += Pause;
        _gameManager.OnResume += Resume;
    }
    private void OnDisable()
    {
        _gameManager.OnPause -= Pause;
        _gameManager.OnResume -= Resume;
    }
    private void Start()
    {
        _speed = _status.Speed;
        _player = GameManager.Player.transform;
    }
    private void Update()
    {
        if (!IsActive || _isPause) return;

        Vector3 dir = _player.position - this.transform.position;
        dir.Normalize();

        transform.position += dir * _speed * Time.deltaTime;
    }

    bool _isActive = false;
    public bool IsActive => _isActive;
    public void DisactiveForInstantiate()
    {
        _sprite.enabled = false;
        _rb.simulated = false;
        _isActive = false;
    }
    public void Create()
    {
        _sprite.enabled = true;
        _sprite.sprite = _status.Sprite;
        _rb.simulated = true;
        _isActive = true;
    }
    public void Destroy()
    {
        _sprite.enabled = false;
        _rb.simulated = false;
        _isActive = false;
    }

    void Pause()
    {
        if(IsActive)
        {
            _rb.simulated = false;
            _isPause = true;
        }
    }
    void Resume()
    {
        if(IsActive)
        {
            _rb.simulated = true;
            _isPause = false;
        }
    }
}
