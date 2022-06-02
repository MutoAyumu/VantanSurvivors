using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("パラメーター")]
    [SerializeField] float _speed = 1f;
    [SerializeField] float _hp = 10f;
    float _currentHp;

    [Header("セットするもの")]
    [SerializeField] Slider _hpBar = default;

    PlayerManager _playerManager;
    SpriteRenderer _sprite;

    float _h;

    private void Awake()
    {
        _playerManager = PlayerManager.Instance;
        _playerManager.SetPlayer(this);

        _sprite = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        _playerManager.SetUp();
        _currentHp = _hp;

        if (_hpBar)
            _hpBar.value = 1;
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

        transform.position += new Vector3(h * _speed * Time.deltaTime, v * _speed * Time.deltaTime, 0);

        _playerManager.Skill.ForEach(s => s.Update());
    }
    public void Damage(int damage)
    {
        _currentHp = Mathf.Clamp(_currentHp - damage, 0, _hp);

        if (!_hpBar) return;
        _hpBar.DOValue(_currentHp / _hp, 0.1f).SetEase(Ease.InOutSine);
    }
    void Flip(float h)
    {
        if (h > 0)
        {
            _sprite.flipX = false;
        }
        else if (h < 0)
        {
            _sprite.flipX = true;
        }
    }
}
