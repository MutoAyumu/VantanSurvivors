using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] Bullet _bullet = default;
    [SerializeField] int _createLimit = 10;

    List<Bullet> _bulletList;

    private void Awake()
    {
        Debug.Log(this + " : CreatePool‚ðŠJŽn");
        CreatePool();
    }

    void CreatePool()
    {
        _bulletList = new List<Bullet>();

        for(int i = 0; i < _createLimit; i++)
        {
            var bu = CreateBullet();
            bu.gameObject.SetActive(false);
            _bulletList.Add(bu);
        }

        Debug.Log(this + $" : {_createLimit}ŒÂ¶¬I—¹");
    }
    Bullet CreateBullet()
    {
        return Instantiate(_bullet, this.transform.position, Quaternion.identity, this.transform);
    }
    public Bullet GetBullet()
    {
        foreach(var obj in _bulletList)
        {
            if(!obj.gameObject.activeSelf)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        var bu = CreateBullet();
        _bulletList.Add(bu);

        return bu;
    }
}
