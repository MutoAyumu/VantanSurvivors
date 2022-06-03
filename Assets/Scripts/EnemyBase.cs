using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IObjectPool, IDamage
{
    [SerializeField] float _radius = 0.5f;

    float _speed;
    float _hp;
    float _power;
    Transform _player = default;
    Animator _anim;
    Rigidbody2D _rb;
    GameManager _gameManager;
    bool _isPause;

    public float Radius { get => _radius;}

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _rb = this.GetComponent<Rigidbody2D>();
        _anim = this.GetComponent<Animator>();
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
        if (!IsActive || _isPause) return;

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
        _anim.Play(status.AnimName);
        _rb.simulated = true;
        _isActive = true;
        _speed = status.Speed;
        _hp = status.Hp;
        _power = status.Power;
    }
    public void Destroy()
    {
        _anim.Play("Enabled");
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

        if(_hp <= 0)
        {
            Destroy();
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
