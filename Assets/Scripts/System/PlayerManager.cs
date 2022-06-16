using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;
using UniRx;

/// <summary>
/// 経験値・レベル・武器の管理
/// </summary>
public class PlayerManager
{
    static PlayerManager _instance = new PlayerManager();
    static public PlayerController Player { get => Instance._player; }

    List<ISkill> _skill = new List<ISkill>();
    List<IPassive> _passive = new List<IPassive>();
    ISpecialSkill _specialSkill = default;
    PlayerController _player = default;

    SkillSelectUI _skillSelect;

    FloatReactiveProperty _exp;
    int _level = 1;
    float _nextLevelUpExp = 1;

    FloatReactiveProperty _specialPoint;

    bool _debugLogFlag;
    bool _levelFlag;

    public List<ISkill> Skill { get => _skill;}
    public List<IPassive> Passive { get => _passive;}
    public ISpecialSkill SpecalSkill { get => _specialSkill; }

    public static PlayerManager Instance { get => _instance;}
    public bool DebugLog { get => _debugLogFlag;}
    public int Level { get => _level;}
    public IReadOnlyReactiveProperty<float> Exp => _exp;

    public float NextLevelUpExp { get => _nextLevelUpExp;}
    public IReadOnlyReactiveProperty<float> SpecialPoint => _specialPoint;

    private PlayerManager() { }

    public void SetUp()
    {
        _skill.Clear();
        _passive.Clear();
        _specialSkill = null;

        AddSkill(1);
        SetSpecialSkill(1);
        _skillSelect = GameObject.FindObjectOfType<SkillSelectUI>();
        _exp = new FloatReactiveProperty(0);
        _specialPoint = new FloatReactiveProperty(0);
        _level = 1;
        _nextLevelUpExp = 1;
    }

    void SetSpecialSkill(int Id)
    {
        if(_specialSkill != null)
        {
            Debug.Log($"<color=yellow>{this}</color> : 必殺技が既にセットされています");
            return;
        }

        ISpecialSkill newSkill = null;

        switch((SpecalSkillDef)Id)
        {
            case SpecalSkillDef.ReflectedLaser:
                newSkill = new ReflectingLaser();
                break;
        }

        if(newSkill != null)
        {
            _specialSkill = newSkill;
            _specialSkill.Setup(_player);
        }
    }
    void AddSkill(int skillId)
    {
        var having = _skill.Where(s => s.SkillId == (SkillDef)skillId);

        if(having.Count() > 0)
        {
            having.Single().Levelup();
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
    void AddPassive(int passiveId)
    {
        var having = _passive.Where(p => p.PassiveId == (PassiveDef)passiveId);

        if (having.Count() > 0)
        {
            having.Single().Levelup();
        }
        else
        {
            IPassive newPassive = null;

            switch ((PassiveDef)passiveId)
            {
                case PassiveDef.SpeedUp:
                    newPassive = new SpeedUpAbility();
                    break;

                case PassiveDef.HitpointUp:
                    newPassive = new HitPointUpAbility();
                    break;

                case PassiveDef.Regenerative:
                    newPassive = new RegenerativeAbility();
                    break;
            }

            if (newPassive != null)
            {
                _player.SetCallBack(newPassive);
                newPassive.Setup();
                _passive.Add(newPassive);
            }
        }
    }
    public void SetPlayer(PlayerController p)
    {
        _player = p;
    }
    //経験値を取得した時に呼ばれる処理
    async void GetExpPoint(int e)
    {
        _exp.Value += e;
        var nextExp = _nextLevelUpExp;
        var currentLevel = _level;
        //一定の経験値が貯まるとレベルをあげる
        //データテーブルを参照

        if(_exp.Value >= nextExp)
        {
            Time.timeScale = 0;
            _levelFlag = true;

            _nextLevelUpExp += 5;
            Debug.Log($"次のレベルアップまでの経験値 : {_nextLevelUpExp}");
            _level++;

            _skillSelect.SelectEvent();

            await UniTask.WaitUntil(() => !_levelFlag);
            await UniTask.Delay(100);

            _exp.Value -= nextExp;

            GetExpPoint(0);
        }

        if (_debugLogFlag)
            Debug.Log($"経験値を取得した : 獲得経験値 {e} : 総合経験値 {_exp}");
    }
    public void SetExpListener(ExpPoint e)
    {
        e.OnGetEvent += GetExpPoint;
    }
    public void GetSpecialPoint(int p)//保護する
    {
        if (_specialSkill.IsAction()) return;

        _specialPoint.Value = Mathf.Clamp(_specialPoint.Value + p, 0f, _player.SpecialValue);
    }


    
    public void SetLogFlag(bool flag)
    {
        _debugLogFlag = flag;
    }
    public void LevelUpSelect(AbilityTable table)
    {
        switch (table.Type)
        {
            case SelectType.Skill:
                AddSkill(table.Id);
                break;

            case SelectType.Passive:
                AddPassive(table.Id);
                break;
        }

        _levelFlag = false;
        Time.timeScale = 1;
    }
}
