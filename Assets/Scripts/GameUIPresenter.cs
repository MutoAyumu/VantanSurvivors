using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameUIPresenter : MonoBehaviour
{
    [Header("ƒvƒŒƒCƒ„[‚ÌHPŠÖŒW")]
    [SerializeField] MVPSlider _hpSlider;
    [SerializeField] float _playerDuration = 0.1f;
    PlayerController _player;

    [Header("ŒoŒ±’lŠÖŒW")]
    [SerializeField] MVPSlider _expSlider;
    [SerializeField] float _expDuration = 0;
    PlayerManager _playerManager;

    [Header("•KŽE‹ZŠÖŒW")]
    [SerializeField] MVPSlider _specialSlider;
    [SerializeField]float _specialDuration = 0;

    private void Start()
    {
        if (_hpSlider)
        {
            _player = PlayerManager.Player;

            _player.CurrentHp.Subscribe(x =>
            {
                _hpSlider.SetValue(x, _player.Hp, _playerDuration);
            }).AddTo(this);
        }

        if(_expSlider)
        {
            _playerManager = PlayerManager.Instance;

            _playerManager.Exp.Subscribe(x =>
            {
                _expSlider.SetValue(x, _playerManager.NextLevelUpExp, _expDuration);
            }).AddTo(this);
        }

        if(_specialSlider)
        {
            _playerManager = PlayerManager.Instance;
            _player = PlayerManager.Player;

            _playerManager.SpecialPoint.Subscribe(x =>
            {
                _specialSlider.SetValue(x, _player.SpecialValue, _specialDuration);
            }).AddTo(this);
        }
    }
}
