using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;

public class EnemyBase : MonoBehaviour, IObjectPool, IDamage
{
    [SerializeField] float _radius = 0.5f;

    float _speed;
    float _hp;
    float _power;
    string _stateName;
    Transform _player = default;
    Animator _anim;
    Rigidbody2D _rb;
    Collider2D _col;
    GameManager _gameManager;
    ItemManager _itemManager;
    PlayerManager _playerManager;
    bool _isPause;
    bool _isDeath;
    ObservableStateMachineTrigger _trigger;

    public float Radius { get => _radius;}

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _itemManager = ItemManager.Instance;
        _playerManager = PlayerManager.Instance;

        _rb = this.GetComponent<Rigidbody2D>();
        _anim = this.GetComponent<Animator>();
        _trigger = _anim.GetBehaviour<ObservableStateMachineTrigger>();
        _col = this.GetComponent<Collider2D>();
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
        _player = PlayerManager.Player.transform;
    }
    private void Update()
    {
        if (!IsActive || _isPause || _isDeath) return;

        Vector3 dir = _player.position - this.transform.position;
        dir.Normalize();

        transform.position += dir * _speed * Time.deltaTime;

        Flip(dir.x);
    }

    bool _isActive = false;
    public bool IsActive => _isActive;
    public void DisactiveForInstantiate()
    {
        _rb.simulated = false;
        _isActive = false;
    }
    public void Create()
    {

    }
    public void Create(EnemyStatus status)
    {
        _stateName = status.AnimName;
        _anim.Play(_stateName);
        _rb.simulated = true;
        _isActive = true;
        _isDeath = false;
        _speed = status.Speed;
        _hp = status.Hp;
        _power = status.Power;
        _col.enabled = true;

        _trigger
            .OnStateExitAsObservable()
            .Where(x => x.StateInfo.IsName(_stateName + "Death"))
            .Subscribe(x =>
            {
                Destroy();
            }).AddTo(this);

        _gameManager.TestObjectCount(true);
    }
    public void Destroy()
    {
        if (_isActive)
        {
            var r = Random.Range(0, 2);

            if (r != 0)
                _itemManager.SetExp(this.transform);

            _playerManager.GetSpecialPoint(1);

            _gameManager.TestObjectCount(false);
            _gameManager.TestEnemyCount();
        }

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
    public void Damage(float damage)
    {
        _hp -= damage;
        DamagePopup.Pop(this.gameObject, (int)damage);

        if(_hp <= 0)
        {
            _anim.SetTrigger("IsDeath");
            _isDeath = true;
            _col.enabled = false;
        }

        if(_gameManager.EnemyDebugLog)
        Debug.Log($"{this.name} : ダメージを受けた({damage}) : 残りHP {_hp}");
    }
    void Flip(float h)
    {
        if (h > 0)
        {

            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        else if (h < 0)
        {

            this.transform.localScale = new Vector3(-1 * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            player.Damage(_power);

            if (_gameManager.EnemyDebugLog)
                Debug.Log($"{this.name} : ダメージを与えた({_power})");
        }
    }
}
