using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MagicBulletSkill : ISkill
{
    float _shotInterval = 1;
    int _skillLevel = 0;
    int _shotCount = 1;

    SkillDef _skillId = SkillDef.ShotBullet;
    ObjectPool<MagicBullet> _bulletPool = new ObjectPool<MagicBullet>();
    Timer _timer = new Timer();

    public SkillDef SkillId { get => _skillId;}

    public void Setup()
    {
        _timer.Setup(_shotInterval);

        var root = new GameObject("MagicBulletRoot").transform;
        var prefab = Resources.Load<MagicBullet>("MagicBullet");
        _bulletPool.SetBaseObj(prefab, root);
        _bulletPool.SetCapacity(100);
    }
    public void Update()
    {
        if(_timer.RunTimer())
        {
            var list = EnemyManager.Enemies;
            EnemyBase[] targets = new EnemyBase[_shotCount];

            for(int i = 0; i < targets.Length; i++)
            {
                //ƒvƒŒƒCƒ„[‚Éˆê”Ô‹ß‚¢“G‚ð’T‚µ‚Ä‚­‚é
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
        _skillLevel++;
        _timer.Setup(_shotInterval);
    }
}
