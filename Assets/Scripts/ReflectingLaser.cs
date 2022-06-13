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

    Vector2 _currentLaserPoint;

    public void Update()
    {
        //�K�E�Z�̏���������

        if (!_isAction) return;

        if (_reflectedCount <= _reflectiveCount)
        {
            _currentLaserPoint = Vector2.Lerp(_currentLaserPoint, _hit.point, _lineAnimationSpeed * Time.deltaTime);

            var delta = _reflectedCount / (float)_reflectiveCount;

            for (int i = _reflectiveCount; i >= _reflectedCount; i--)
            {
                _line.SetPosition(i, Vector2.Lerp(_currentLaserPoint, _hit.point, delta));
            }

            var ray = Physics2D.LinecastAll(_line.GetPosition(_reflectedCount), _currentLaserPoint);

            foreach(var go in ray)
            {
                var e = go.collider.GetComponent<IDamage>();
                e?.Damage(10);
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

        _lineAnimationSpeed = setup.LineAnimationSpeed;
        _reflectiveCount = setup.ReflectiveCount;
        _mask = setup.Mask;
        _changeDistance = setup.ChangeDistance;

        _line.alignment = LineAlignment.TransformZ;
        _line.numCapVertices = _reflectiveCount;
        _line.numCornerVertices = _reflectiveCount;
        _line.material = setup.Material;

        Debug.Log($"<color=yellow>{this}</color> : �K�E�Z�̒ǉ�");
    }
    public void Use()
    {
        //�A�N�V�������n�߂鏈��

        if (_isAction) return;

        var origin = PlayerManager.Player.transform.position;

        if (!_isAction)
        {
            _line.positionCount = _reflectiveCount + 1;
            _line.SetPosition(0, origin);
            _line.startWidth = 0.5f;
            _reflectedCount++;
        }

        //�����_���ȕ������쐬
        var randam = Random.Range(360f, 0f);

        Vector3 dir = new Vector3(0, 0, 0);
        dir.x = origin.x + Mathf.Cos(randam);
        dir.y = origin.y + Mathf.Sin(randam);

        //�v���C���[�̈ʒu���烉���_���ȕ�����Ray���΂�
        _hit = Physics2D.Raycast(origin, dir.normalized, 100, _mask);

        _isAction = true;
        Debug.Log($"<color=yellow>{this}</color> : ���˃��[�U�[���g�p");
    }
    void Reflection()
    {
        //���˂�������
        var dir = _hit.point - (Vector2)_line.GetPosition(_reflectedCount - 1);
        var reflect = Vector2.Reflect(dir, _hit.normal);

        _hit = Physics2D.Raycast(_currentLaserPoint, reflect, 100,_mask);

        _reflectedCount++;
    }
}
[CreateAssetMenu]
public class LaserSetup : ScriptableObject
{
    public int ReflectiveCount = 11;
    public float LineAnimationSpeed = 20f;
    public float ChangeDistance = 0.1f;
    public LayerMask Mask;
    public Material Material;
}
