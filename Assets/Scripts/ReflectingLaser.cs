using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectingLaser : ISpecialSkill
{
    int _reflectiveCount = 5;
    int _reflectedCount;
    float _lineAnimationSpeed = 20f;
    float _changeDistance = 0.2f;

    bool _isAction;
    LineRenderer _line;
    RaycastHit2D _hit;

    Vector2 _currentLaserPoint;
    Vector2 _offset;

    public void Update()
    {
        //�K�E�Z�̏���������

        if (!_isAction) return;

        _currentLaserPoint = Vector2.Lerp(_currentLaserPoint, _hit.point, _lineAnimationSpeed * Time.deltaTime);

        var delta = _reflectedCount / (float)_reflectiveCount;

        _line.SetPosition(_reflectedCount, Vector2.Lerp(_hit.point, _currentLaserPoint, delta) + _offset);

        var distance = Vector2.Distance(_currentLaserPoint, _hit.point);

        if (distance <= _changeDistance)
        {
            if (_reflectedCount < _reflectiveCount)
            {
                _reflectedCount++;
                Reflection();
            }
            else
            {
                _line.positionCount = 0;
                _isAction = false;
            }
        }
    }

    public void Setup(PlayerController player)
    {
        _line = player.gameObject.AddComponent<LineRenderer>();
        Debug.Log($"<color=yellow>{this}</color> : �K�E�Z�̒ǉ�");
    }
    public void Use()
    {
        //�A�N�V�������n�߂鏈��
        if(!_isAction)
        {
            if(_line.positionCount == 0)
            {
                _line.positionCount = _reflectiveCount + 1;
            }
        }

        var origin = PlayerManager.Player.transform.position;

        //�����_���ȕ������쐬
        var randam = Random.Range(360f, 0f);

        Vector3 dir = new Vector3(0, 0, 0);
        dir.x = origin.x + Mathf.Cos(randam);
        dir.y = origin.y + Mathf.Sin(randam);

        //�v���C���[�̈ʒu���烉���_���ȕ�����Ray���΂�
        _hit = Physics2D.Raycast(PlayerManager.Player.transform.position, dir, 100);

        //�I�t�Z�b�g�̐ݒ�
        _offset = Vector2.up;
        _offset.y = _hit.point.y;

        _isAction = true;
        Debug.Log($"<color=yellow>{this}</color> : ���˃��[�U�[���g�p");
    }
    void Reflection()
    {
        //���˂�������
        Debug.Log($"<color=yellow>{this}</color> : ����");
    }
}
