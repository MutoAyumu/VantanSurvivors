using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : BulletBase
{
    public override void Shoot(EnemyBase enemy)
    {
        var dir = enemy.transform.position - PlayerManager.Player.transform.position;
        _rb.velocity = dir.normalized * _speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<EnemyBase>())
        {
            Destroy();
        }
    }
}
