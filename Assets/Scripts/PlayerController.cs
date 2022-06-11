using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

public class PlayerController : MonoBehaviour
{
    [Header("パラメーター")]
    [SerializeField] float _speed = 1f;
    [SerializeField] float _hp = 10f;
    FloatReactiveProperty _currentHp;
    float _currentSpeed;
    public IReadOnlyReactiveProperty<float> CurrentHp => _currentHp;

    public float Hp { get => _hp;}

    [Header("セットするもの")]
    [SerializeField] bool _isDebugLog;
    [SerializeField] bool _isGodMode;

    PlayerManager _playerManager;
    SpriteRenderer _sprite;

    float _h;

    private void Awake()
    {
        _playerManager = PlayerManager.Instance;
        _playerManager.SetPlayer(this);

        _sprite = GetComponent<SpriteRenderer>();

        _currentHp = new FloatReactiveProperty(_hp);

        _playerManager.SetUp();
        _playerManager.SetLogFlag(_isDebugLog);

        _currentSpeed = _speed;
    }
    private void Start()
    {

    }
    private void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        if (h != 0)
        {
            _h = h;
        }

        Flip(_h);

        transform.position += new Vector3(h, v, 0).normalized * _currentSpeed * Time.deltaTime;

        _playerManager.Skill.ForEach(s => s.Update());
        _playerManager.Passive.ForEach(p => p.Update());
    }
    public void Damage(float damage)
    {
        if (!_isGodMode)
        {
            _currentHp.Value = Mathf.Clamp(_currentHp.Value - damage, 0, _hp);
        }

        if(_playerManager.DebugLog)
        Debug.Log($"{this.name} : ダメージを受けた({damage}) : 残りHP {_currentHp}");
    }
    void Flip(float h)
    {
        if (Time.timeScale < 1) return;

        if (h > 0)
        {
            _sprite.flipX = false;
        }
        else if (h < 0)
        {
            _sprite.flipX = true;
        }
    }
    public void SetCallBack(IPassive ability)
    {
        switch(ability.PassiveId)
        {
            case PassiveDef.SpeedUp:
                ability._event += SpeedUp;
                break;

            case PassiveDef.HitpointUp:
                ability._event += HitPointUp;
                break;

            case PassiveDef.Regenerative:
                ability._event += Regenerative;
                break;
        }
    }
    //ここからパッシブでやらせたいこと
    //ハイパーマジックナンバー
    
    void SpeedUp()
    {
        _currentSpeed += 0.2f;
    }
    void Regenerative()
    {
        if (_currentHp.Value > _hp) return;

        _currentHp.Value += 1;
    }
    void HitPointUp()
    {
        _hp++;
    }
}
