using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MagicBulletSkill : ISkill
{
    float _shotInterval;
    int _skillLevel = 1;
    int _shotCount = 1;

    SkillDef _skillId = SkillDef.MagicBullet;
    ObjectPool<MagicBullet> _bulletPool = new ObjectPool<MagicBullet>();
    Timer _timer = new Timer();
    GameManager _gameManager;

    public SkillDef SkillId { get => _skillId;}

    public void Setup()
    {
        _shotInterval = 0.5f * 6;
        _timer.Setup(_shotInterval);
        _gameManager = GameManager.Instance;

        var root = new GameObject("MagicBulletRoot").transform;
        var prefab = Resources.Load<MagicBullet>("MagicBullet");
        _bulletPool.SetBaseObj(prefab, root);
        _bulletPool.SetCapacity(100);
        Debug.Log($"<color=yellow>{this}</color> : スキルの追加");
    }
    public void Update()
    {
        if(_timer.RunTimer())
        {
            var list = _gameManager.Enemies;

            for(int i = 0; i < _shotCount; i++)
            {
                //プレイヤーに一番近い敵を探してくる
                var target = list.Where(e => e.IsActive).OrderBy(e => 
                Vector2.Distance(PlayerManager.Player.transform.position, e.transform.position)).FirstOrDefault();

                var script = _bulletPool.Instantiate();
                script.transform.position = PlayerManager.Player.transform.position;
                script.Shoot(target);
            }

        }
    }
    public void Levelup()
    {
        if (_skillLevel > 5) return;

        _skillLevel++;
        _shotInterval -= 0.5f;
        _timer.Setup(Mathf.Max(_shotInterval, 0.5f));
        Debug.Log($"<color=yellow>{this}</color> : レベルアップ{_skillLevel}");
    }
}
