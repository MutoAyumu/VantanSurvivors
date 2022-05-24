using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAreaSkill : ISkill
{
    int _skillLevel = 0;
    float _interval = 1f;
    int _maxAttackCount = 10;
    float _area = 2.5f;
    float _damage = 1f;

    SkillDef _skillId = SkillDef.DamageArea;
    Timer _timer = new Timer();

    public SkillDef SkillId { get => _skillId; }

    public void Setup()
    {
        _timer.Setup(_interval);
        Debug.Log($"<color=yellow>{this}</color> : スキルの追加");
    }
    public void Update()
    {
        if (_timer.RunTimer())
        {
            var attackCount = 0;

            var enemies = Physics2D.OverlapCircleAll(PlayerManager.Player.transform.position, _area);

            foreach(var e in enemies)
            {
                var target = e.GetComponent<IDamage>();

                if (target == null) continue;

                target.Damage(_damage);
                attackCount++;

                if(attackCount >= _maxAttackCount)
                {
                    break;
                }
            }
        }
    }
    public void Levelup()
    {
        _skillLevel++;
        _area += 0.1f;
        Mathf.Min(_area, 5f);
        Debug.Log($"<color=yellow>{this}</color> : レベルアップ{_skillLevel}");
    }
}
