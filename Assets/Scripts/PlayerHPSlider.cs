using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHPSlider : MonoBehaviour
{
    Slider _hpBar = default;

    private void Awake()
    {
        _hpBar = GetComponent<Slider>();
    }

    public void SetValue(float value, float maxValue)
    {
        if (_hpBar)
            _hpBar.DOValue(value / maxValue, 0.1f).SetEase(Ease.InOutSine);
    }
}
