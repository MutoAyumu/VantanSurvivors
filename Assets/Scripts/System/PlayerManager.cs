using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// �o���l�E���x���E����̊Ǘ�
/// </summary>
public class PlayerManager
{
    static PlayerManager _instance = new PlayerManager();
    static public PlayerController Player { get => Instance._player; }

    List<ISkill> _skill = new List<ISkill>();
    PlayerController _player = default;

    [SerializeField] int _startSkill = 1;

    int _exp;
    int _level = 1;

    bool _debugLogFlag;

    public List<ISkill> Skill { get => _skill;}
    public static PlayerManager Instance { get => _instance;}
    public bool DebugLog { get => _debugLogFlag;}

    private PlayerManager() { }

    public void SetUp()
    {
        AddSkill(_startSkill);
    }

    public void SetSkillListener(CustomButton b)
    {
        var button = b;
        button.OnClickCallback += AddSkill;
    }

    void AddSkill(int skillId)
    {
        var having = _skill.Where(s => s.SkillId == (SkillDef)skillId);

        if(having.Count() > 0)
        {
            having.Single().Levelup();  //�������܂���������Ȃ����璲�ׂ�
        }
        else
        {
            ISkill newSkill = null;

            switch((SkillDef)skillId)
            {
                case SkillDef.MagicBullet:
                    newSkill = new MagicBulletSkill();
                    break;

                case SkillDef.DamageArea:
                    newSkill = new DamageAreaSkill();
                    break;

                case SkillDef.ShotBullet:
                    newSkill = new ShotBulletSkill();
                    break;

                case SkillDef.ReflectingBullet:
                    newSkill = new ReflectedBulletSkill();
                    break;
            }

            if(newSkill != null)
            {
                newSkill.Setup();
                _skill.Add(newSkill);
            }
        }
    }
    public void SetPlayer(PlayerController p)
    {
        _player = p;
    }

    //�o���l���擾�������ɌĂ΂�鏈��
    void GetExpPoint(int e)
    {
        _exp += e;

        //���̌o���l�����܂�ƃ��x����������
        //�f�[�^�e�[�u�����Q��

        if (_debugLogFlag)
            Debug.Log($"�o���l���擾���� : �l���o���l {e} : �����o���l {_exp}");
    }
    public void SetExpListener(ExpPoint e)
    {
        e.OnGetEvent += GetExpPoint;
    }
    
    public void SetLogFlag(bool flag)
    {
        _debugLogFlag = flag;
    }
}
