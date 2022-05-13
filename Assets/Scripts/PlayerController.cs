using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] BulletPool _bulletPool = default;

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shot();
        }
    }
    void Shot()
    {
        var bu = _bulletPool.GetBullet();
        bu.transform.position = this.transform.position;
    }
}
