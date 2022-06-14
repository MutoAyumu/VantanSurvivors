using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MVPSlider : MonoBehaviour
{
    Slider _slider = default;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void SetValue(float value, float maxValue, float time)
    {
        if (_slider)
            _slider.DOValue(value / maxValue, time).SetEase(Ease.InOutSine);
    }
}
