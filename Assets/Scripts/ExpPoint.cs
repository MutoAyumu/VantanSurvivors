using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �o���l�p�N���X
/// </summary>
public class ExpPoint : ItemBase
{
    [SerializeField] int _exp = 1;

    public int Exp { get => _exp;}

    public event System.Action<int> OnGetEvent;

    protected override void OnAwake()
    {
        PlayerManager.Instance.SetExpListener(this);
    }
    protected override void AddProcess()
    {
        //�����Ńv���C���[�̌o���l��ǉ�
        OnGetEvent?.Invoke(_exp);
    }
}
