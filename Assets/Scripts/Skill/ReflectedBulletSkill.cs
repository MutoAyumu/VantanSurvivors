using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectedBulletSkill : ISkill
{
    float _shotInterval;
    int _skillLevel = 0;
    int _shotCount = 1;

    SkillDef _skillId = SkillDef.ReflectingBullet;
    ObjectPool<ReflectingBullets> _bulletPool = new ObjectPool<ReflectingBullets>();
    Timer _timer = new Timer();

    public SkillDef SkillId { get => _skillId; }

    public void Setup()
    {
        _shotInterval = 0.5f * 6;
        _timer.Setup(_shotInterval);

        var root = new GameObject("ReflectionBulletRoot").transform;
        var prefab = Resources.Load<ReflectingBullets>("ReflectionBullet");
        _bulletPool.SetBaseObj(prefab, root);
        _bulletPool.SetCapacity(100);
        Debug.Log($"<color=yellow>{this}</color> : スキルの追加");
    }
    public void Update()
    {
        if (_timer.RunTimer())
        {
            var cRad = Random.Range(360f, 0f);
            Vector3 dir = new Vector3(0, 0, 0);

            var script = _bulletPool.Instantiate();
            script.transform.position = PlayerManager.Player.transform.position;

            dir.x = script.transform.position.x + Mathf.Cos(cRad);
            dir.y = script.transform.position.y + Mathf.Sin(cRad);

            script.Shoot(dir);
        }
    }
    public void Levelup()
    {
        if (_skillLevel >= 5) return;

        _skillLevel++;
        _shotInterval -= 0.5f;
        _timer.Setup(Mathf.Max(_shotInterval, 0.5f));
        Debug.Log($"<color=yellow>{this}</color> : レベルアップ{_skillLevel}");
    }
}
