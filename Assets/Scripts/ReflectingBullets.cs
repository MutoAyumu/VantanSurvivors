using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectingBullets : BulletBase
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
        }
        else
        {
            //�ڐG�����|�C���g���擾
            var hitPos = collision.ClosestPoint(this.transform.position);

            var normal = hitPos - (Vector2)this.transform.position;
            var direction = this.transform.up;
            //���˃x�N�g���̍쐬
            this.transform.up = Vector2.Reflect(direction, normal.normalized);
        }
    }
}
