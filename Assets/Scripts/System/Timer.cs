using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    float _timer;
    float _interval = 1f;

    public void Setup(float time)
    {
        _interval = time;
    }
    public bool RunTimer()
    {
        _timer += Time.deltaTime;

        if(_timer >= _interval)
        {
            _timer = 0;
            return true;
        }

        return false;
    }
    public float ReturnTime()
    {
        return _timer;
    }
}
