using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MVPSlider : MonoBehaviour
{
    Slider _Slider = default;

    private void Awake()
    {
        _Slider = GetComponent<Slider>();
    }

    public void SetValue(float value, float maxValue, float time)
    {
        if (_Slider)
            _Slider.DOValue(value / maxValue, time).SetEase(Ease.InOutSine);
    }
}
