using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBullet : ISkill
{
    float _shotInterval = 1;
    int _skillLevel = 0;

    SkillDef _skillId = default;
    ObjectPool<BulletBase> _bulletPool = new ObjectPool<BulletBase>();
    Timer _timer = new Timer();

    public SkillDef SkillId { get => _skillId;}

    public void Setup()
    {
        _timer.Setup(_shotInterval);
    }
    public void Update()
    {
        if(_timer.RunTimer())
        {

        }
    }
    public void Levelup()
    {
        _skillLevel++;
        _timer.Setup(_shotInterval);
    }
}
