using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolBase : MonoBehaviour
{
    [SerializeField] EnemyBase _enemy = default;
    [SerializeField] int _createLimit = 10;

    List<EnemyBase> _enemyList;

    private void Awake()
    {
        CreatePool();
    }

    void CreatePool()
    {
        _enemyList = new List<EnemyBase>();

        for (int i = 0; i < _createLimit; i++)
        {
            var e = CreateEnemy();
            e.Rb.simulated = false;
            e.gameObject.SetActive(false);
            _enemyList.Add(e);
        }
    }
    EnemyBase CreateEnemy()
    {
        return Instantiate(_enemy, this.transform.position, Quaternion.identity, this.transform);
    }
    public EnemyBase GetEnemy()
    {
        foreach (var obj in _enemyList)
        {
            if (!obj.Rb.simulated)
            {
                obj.Rb.simulated = true;
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        var e = CreateEnemy();
        _enemyList.Add(e);

        e.Rb.simulated = true;
        return e;
    }
}
