using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// �o���l�E���x���E����̊Ǘ�
/// </summary>
public class PlayerManager : Singleton<PlayerManager>
{
    List<ISkill> _skill = new List<ISkill>();

    public List<ISkill> Skill { get => _skill;}

    protected override void OnAwake()
    {
        AddSkill(1);
    }
    public void AddSkill(int skillId)
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
                case SkillDef.ShotBullet:
                    newSkill = new ShotBullet();
                    break;

                case SkillDef.DamageArea:
                    break;
            }

            if(newSkill != null)
            {
                newSkill.Setup();
                _skill.Add(newSkill);
            }
        }
    }
}
