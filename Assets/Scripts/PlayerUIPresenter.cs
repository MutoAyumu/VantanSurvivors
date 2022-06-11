using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerUIPresenter : MonoBehaviour
{
    PlayerController _player;
    [SerializeField] PlayerHPSlider _hpSlider;

    private void Start()
    {
        _player = PlayerManager.Player;

        _player.CurrentHp.Subscribe(x =>
        {
            _hpSlider.SetValue(x, _player.Hp);
        }).AddTo(this);
    }
}
