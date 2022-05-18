using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float _time = 0.05f;
    [SerializeField] int _createLimit = 1000;
    [SerializeField] float _lenght = 50;
    [SerializeField] EnemyBase _prefab = null;
    [SerializeField] Transform _root = null;

    float _timer = 0.0f;
    float _cRad = 0.0f;
    Vector3 _popPos = new Vector3(0, 0, 0);

    ObjectPool<EnemyBase> _enemyPool = new ObjectPool<EnemyBase>();

    private void Start()
    {
        _enemyPool.SetBaseObj(_prefab, _root);
        _enemyPool.SetCapacity(_createLimit);
        EnemyManager.Instance.SetList(_enemyPool.PoolList);

        for (int i = 0; i < _createLimit; i++) Spawn();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _time)
        {
            Spawn();
            _timer -= _time;
        }
    }

    void Spawn()
    {
        var script = _enemyPool.Instantiate();
        if (!script) return;
        /*
        var go = GameObject.Instantiate(_prefab);
        var script = go.GetComponent<Enemy>();
        */
        _popPos.x = GameManager.Player.transform.position.x + _lenght * Mathf.Cos(_cRad);
        _popPos.y = GameManager.Player.transform.position.y + _lenght * Mathf.Sin(_cRad);
        script.transform.position = _popPos;
        _cRad += 0.1f;
    }
}