using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAreaSkill : ISkill
{
    int _skillLevel = 1;
    float _interval = 1f;
    int _maxAttackCount = 10;
    float _area = 1.5f;
    float _damage = 1f;

    SkillDef _skillId = SkillDef.DamageArea;
    Timer _timer = new Timer();
    GameObject _areaImage;

    public SkillDef SkillId { get => _skillId; }

    public void Setup()
    {
        _timer.Setup(_interval);

        _areaImage = GameObject.Instantiate(Resources.Load("DamageArea"), PlayerManager.Player.transform) as GameObject;
        _areaImage.transform.localScale = new Vector3(_area * 2, _area * 2, 0);
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
        if (_skillLevel > 5) return;

        _skillLevel++;
        _area += 0.2f;
        _areaImage.transform.localScale = new Vector3(_area * 2, _area * 2, 0);
        Debug.Log($"<color=yellow>{this}</color> : レベルアップ{_skillLevel}");
    }
}
