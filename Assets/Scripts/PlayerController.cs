using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] BulletPool _bulletPool = default;
    [SerializeField] float _speed = 1f;

    private void Start()
    {
        GameManager.Instance.SetPlayer(this);
    }
    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shot();
        }

        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(h * _speed * Time.deltaTime, v * _speed * Time.deltaTime, 0);
    }
    void Shot()
    {
        var bu = _bulletPool.GetBullet();
        bu.transform.position = this.transform.position;
    }
}
