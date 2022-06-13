using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectingLaser : ISpecialSkill
{
    int _reflectiveCount = 11;
    int _reflectedCount;
    float _lineAnimationSpeed = 20f;
    float _changeDistance = 0.1f;

    bool _isAction;
    LineRenderer _line;
    RaycastHit2D _hit;
    LayerMask _mask;

    Vector2 _currentLaserPoint;

    Transform test;

    public void Update()
    {
        //必殺技の処理を書く

        if (!_isAction) return;

        if (_reflectedCount <= _reflectiveCount)
        {
            _currentLaserPoint = Vector2.Lerp(_currentLaserPoint, _hit.point, _lineAnimationSpeed * Time.deltaTime);

            var delta = _reflectedCount / (float)_reflectiveCount;

            for (int i = _reflectiveCount; i >= _reflectedCount; i--)
            {
                _line.SetPosition(i, Vector2.Lerp(_currentLaserPoint, _hit.point, delta));
            }

            //_line.SetPosition(_reflectedCount, Vector2.Lerp(_currentLaserPoint, _hit.point, delta));

            var distance = Vector2.Distance(_currentLaserPoint, _hit.point);

            if (distance <= _changeDistance)
            {
                Reflection();
            }
        }
        else
        {
            _line.positionCount = 0;
            _reflectedCount = 0;
            _currentLaserPoint = Vector2.zero;
            _isAction = false;
        }
    }

    public void Setup(PlayerController player)
    {
        _line = player.gameObject.AddComponent<LineRenderer>();
        _line.startWidth = 0.5f;
        _line.alignment = LineAlignment.TransformZ;
        _line.numCapVertices = 90;
        _line.numCornerVertices = 90;
        _line.material = new Material(Shader.Find("Sprites/Default"));

        _mask = LayerMask.GetMask("Water");
        test = GameObject.Find("AAAAA").transform;
        Debug.Log($"<color=yellow>{this}</color> : 必殺技の追加");
    }
    public void Use()
    {
        //アクションを始める処理

        var origin = PlayerManager.Player.transform.position;

        if (!_isAction)
        {
            _line.positionCount = _reflectiveCount + 1;
            _line.SetPosition(0, origin);
            _reflectedCount++;
        }

        //ランダムな方向を作成
        var randam = Random.Range(360f, 0f);

        Vector3 dir = new Vector3(0, 0, 0);
        dir.x = origin.x + Mathf.Cos(randam);
        dir.y = origin.y + Mathf.Sin(randam);

        //プレイヤーの位置からランダムな方向にRayを飛ばす
        _hit = Physics2D.Raycast(origin, dir.normalized, 100, _mask);

        _isAction = true;
        Debug.Log($"<color=yellow>{this}</color> : 反射レーザーを使用");
    }
    void Reflection()
    {
        //反射した処理
        var dir = _hit.point - (Vector2)_line.GetPosition(_reflectedCount - 1);
        var reflect = Vector2.Reflect(dir, _hit.normal);

        _hit = Physics2D.Raycast(_currentLaserPoint, reflect, 100,_mask);

        test.position = _hit.point;
        Debug.Log(_hit.collider.name);

        _reflectedCount++;
        Debug.Log($"<color=yellow>{this}</color> : 反射");
    }
}
