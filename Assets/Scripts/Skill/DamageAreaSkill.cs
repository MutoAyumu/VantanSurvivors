using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DamageAreaSkill : ISkill
{
    int _skillLevel = 1;
    float _interval = 1f;
    int _maxAttackCount = 10;
    float _maxArea = 1.5f;
    float _damage = 1f;

    SkillDef _skillId = SkillDef.DamageArea;
    Timer _timer = new Timer();
    GameObject _areaImage;
    AudioClip _clip;

    public SkillDef SkillId { get => _skillId; }

    public void Setup()
    {
        _timer.Setup(_interval);

        _clip = Resources.Load<AudioClip>("Electronic");

        _areaImage = GameObject.Instantiate(Resources.Load("DamageArea"), PlayerManager.Player.transform) as GameObject;
        _areaImage.transform.localScale = new Vector3(_maxArea * 2, _maxArea * 2, 0);
        Debug.Log($"<color=yellow>{this}</color> : スキルの追加");

        _areaImage.transform.DOLocalRotate(new(0, 0, 360f), 5f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

        _areaImage.transform.localScale = Vector3.zero;
        _areaImage.transform.DOScale(new Vector3(_maxArea, _maxArea, 0), 1f)
            .SetEase(Ease.Linear);
    }
    public void Update()
    {
        if (_timer.RunTimer())
        {
            var attackCount = 0;

            var enemies = Physics2D.OverlapCircleAll(PlayerManager.Player.transform.position, _maxArea / 2);

            foreach (var e in enemies)
            {
                var target = e.GetComponent<IDamage>();

                if (target == null) continue;

                target.Damage(_damage);
                attackCount++;

                PlayerManager.Player.SoundPlay(_clip);

                if (attackCount >= _maxAttackCount)
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
        _maxArea += 0.25f;

        _areaImage.transform.DOScale(new Vector3(_maxArea, _maxArea, 0), 1f)
            .SetEase(Ease.Linear);

        Debug.Log($"<color=yellow>{this}</color> : レベルアップ{_skillLevel}");
    }
}
