using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBulletSkill : ISkill
{
    float _shotInterval = 2f;
    int _skillLevel = 0;
    int _shotCount = 1;

    SkillDef _skillId = SkillDef.ShotBullet;
    ObjectPool<ShotBullet> _bulletPool = new ObjectPool<ShotBullet>();
    Timer _timer = new Timer();

    Vector2[] _vectors = new Vector2[4] { new Vector2(1, 1), new Vector2(-1, 1), new Vector2(1, -1), new Vector2(-1, -1)};

    public SkillDef SkillId { get => _skillId; }

    public void Setup()
    {
        _timer.Setup(_shotInterval);

        var root = new GameObject("ShotBulletRoot").transform;
        var prefab = Resources.Load<ShotBullet>("ShotBullet");
        _bulletPool.SetBaseObj(prefab, root);
        _bulletPool.SetCapacity(100);
        Debug.Log($"<color=yellow>{this}</color> : スキルの追加");
    }
    public void Update()
    {
        if (_timer.RunTimer())
        {
            for(int i = 0; i < _shotCount; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    var script = _bulletPool.Instantiate();
                    script.transform.position = PlayerManager.Player.transform.position;
                    script.Shoot(_vectors[j]);
                }
            }
        }
    }
    public void Levelup()
    {
        _skillLevel++;
        _shotInterval -= 0.5f;
        _timer.Setup(Mathf.Max(_shotInterval, 0.5f));
        Debug.Log($"<color=yellow>{this}</color> : レベルアップ{_skillLevel}");
    }
}
