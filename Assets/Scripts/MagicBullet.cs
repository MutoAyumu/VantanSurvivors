using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : BulletBase
{
    EnemyBase _target;

    public void Shoot(EnemyBase enemy)
    {
        if (_target != null) return;

        _target = enemy;
    }
    protected override void OnUpdate()
    {
        Vector3 dir = default;

        if (_target)
        {
            dir = _target.transform.position - this.transform.position;
            this.transform.up = dir;

            if (!_target.IsActive)
            {
                _target = null;
            }
        }
        else
        {
            dir = this.transform.up;
        }

        transform.position += dir.normalized * _speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<EnemyBase>())
        {
            var e = collision.GetComponent<IDamage>();

            if(e != null)
            {
                e.Damage(1);
            }

            _target = null;
            Destroy();
        }
    }
}
