using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBullet : BulletBase
{
    public void Shoot(Vector2 dir)
    {
        this.transform.up = dir.normalized;
    }

    protected override void OnUpdate()
    {
        transform.position += this.transform.up * _speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyBase>())
        {
            var e = collision.GetComponent<IDamage>();

            if (e != null)
            {
                e.Damage(1);
            }

            Destroy();
        }
    }
}
