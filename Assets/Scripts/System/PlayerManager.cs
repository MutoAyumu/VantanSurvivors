using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 経験値・レベル・武器の管理
/// </summary>
public class PlayerManager
{
    static PlayerManager _instance = new PlayerManager();
    static public PlayerController Player { get => Instance._player; }

    List<ISkill> _skill = new List<ISkill>();
    PlayerController _player = default;

    SkillSelectUI _skillSelect;

    int _exp;
    int _level = 1;
    int _nextLevelUpExp = 10;

    bool _debugLogFlag;

    public List<ISkill> Skill { get => _skill;}
    public static PlayerManager Instance { get => _instance;}
    public bool DebugLog { get => _debugLogFlag;}
    public int Level { get => _level; set => _level = value; }

    private PlayerManager() { }

    public void SetUp()
    {
        AddSkill(1);
        _skillSelect = GameObject.FindObjectOfType<SkillSelectUI>();
    }

    void AddSkill(int skillId)
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

    //経験値を取得した時に呼ばれる処理
    void GetExpPoint(int e)
    {
        _exp += e;
        var nextExp = _nextLevelUpExp;
        //一定の経験値が貯まるとレベルをあげる
        //データテーブルを参照

        if(_exp >= nextExp)
        {
            _nextLevelUpExp += _nextLevelUpExp;
            _level++;
        }

        if (_debugLogFlag)
            Debug.Log($"経験値を取得した : 獲得経験値 {e} : 総合経験値 {_exp}");
    }
    public void SetExpListener(ExpPoint e)
    {
        e.OnGetEvent += GetExpPoint;
    }
    
    public void SetLogFlag(bool flag)
    {
        _debugLogFlag = flag;
    }
    public void LevelUpSelect(SkillTable table)
    {
        switch (table.Type)
        {
            case SelectType.Skill:
                AddSkill(table.Id);
                break;

            case SelectType.Passive:
                break;
        }
    }
}
