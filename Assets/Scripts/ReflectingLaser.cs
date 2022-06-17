using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ReflectingLaser : ISpecialSkill
{
    int _reflectiveCount;
    int _reflectedCount;
    float _lineAnimationSpeed;
    float _changeDistance;

    bool _isAction;
    LineRenderer _line;
    RaycastHit2D _hit;
    LayerMask _mask;
    AudioClip _clip;

    Vector2 _currentLaserPoint;

    public void Update()
    {
        //必殺技の処理を書く

        if (!_isAction) return;

        if (_reflectedCount <= _reflectiveCount)
        {
            _currentLaserPoint = Vector2.Lerp(_currentLaserPoint, _hit.point, _lineAnimationSpeed * Time.deltaTime);

            //var delta = _reflectedCount / (float)_reflectiveCount;

            for (int i = _reflectiveCount; i >= _reflectedCount; i--)
            {
                _line.SetPosition(i, Vector2.Lerp(_currentLaserPoint, _hit.point, Time.deltaTime));
            }

            for(int i = 1; i <= _reflectedCount; i++)
            {
                var ray = Physics2D.LinecastAll(_line.GetPosition(i  - 1), _line.GetPosition(i));

                foreach (var go in ray)
                {
                    var e = go.collider.GetComponent<IDamage>();
                    e?.Damage(10);
                }
            }

            var distance = Vector2.Distance(_currentLaserPoint, _hit.point);

            if (distance <= _changeDistance)
            {
                Reflection();
            }
        }
        else
        {
            DOVirtual.Float(_line.startWidth, 0f, 1f, value => _line.startWidth = value)
                .OnComplete(() =>
                {
                    _line.positionCount = 0;
                    _reflectedCount = 0;
                    _currentLaserPoint = Vector2.zero;
                    _isAction = false;
                });
        }
    }

    public void Setup(PlayerController player)
    {
        _line = player.gameObject.AddComponent<LineRenderer>();

        var setup = Resources.Load<LaserSetup>("LaserSetup");
        _clip = Resources.Load<AudioClip>("Laser");

        _lineAnimationSpeed = setup.LineAnimationSpeed;
        _reflectiveCount = setup.ReflectiveCount;
        _mask = setup.Mask;
        _changeDistance = setup.ChangeDistance;

        _line.alignment = LineAlignment.TransformZ;
        _line.numCapVertices = _reflectiveCount;
        _line.numCornerVertices = _reflectiveCount;
        _line.material = setup.Material;

        Debug.Log($"<color=yellow>{this}</color> : 必殺技の追加");
    }
    public void Use()
    {
        //アクションを始める処理

        if (_isAction) return;

        var origin = PlayerManager.Player.transform.position;

        if (!_isAction)
        {
            _line.positionCount = _reflectiveCount + 1;
            _line.SetPosition(0, origin);
            _line.startWidth = 0.5f;
            _reflectedCount++;
        }

        //ランダムな方向を作成
        var randam = Random.Range(360f, 0f);

        Vector3 dir = new Vector3(0, 0, 0);
        dir.x = origin.x + Mathf.Cos(randam);
        dir.y = origin.y + Mathf.Sin(randam);

        //プレイヤーの位置からランダムな方向にRayを飛ばす
        _hit = Physics2D.Raycast(origin, dir.normalized, 100, _mask);

        PlayerManager.Player.SoundPlay(_clip);

        _isAction = true;
        Debug.Log($"<color=yellow>{this}</color> : 反射レーザーを使用");
    }
    void Reflection()
    {
        //反射する処理
        var dir = _hit.point - (Vector2)_line.GetPosition(_reflectedCount - 1);
        var reflect = Vector2.Reflect(dir, _hit.normal);

        _hit = Physics2D.Raycast(_currentLaserPoint, reflect, 100,_mask);

        PlayerManager.Player.SoundPlay(_clip);

        _reflectedCount++;
    }
    public bool IsAction()
    {
        return _isAction;
    }
}