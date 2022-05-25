using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 経験値・レベル・武器の管理
/// </summary>
public class PlayerManager : Singleton<PlayerManager>
{
    List<ISkill> _skill = new List<ISkill>();
    PlayerController _player = default;
    [SerializeField] int _startSkill = 1;

    static public PlayerController Player { get => Instance._player; }

    public List<ISkill> Skill { get => _skill;}

    protected override void OnAwake()
    {
        AddSkill(_startSkill);
    }
    public void AddSkill(int skillId)
    {
        var having = _skill.Where(s => s.SkillId == (SkillDef)skillId);

        if(having.Count() > 0)
        {
            having.Single().Levelup();  //ここいまいち分からないから調べる
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
}
