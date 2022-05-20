using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵のリストを所持・配置を管理
/// </summary>
public class EnemyManager : Singleton<EnemyManager>
{
    List<EnemyBase> _enemies = new List<EnemyBase>();
    [SerializeField] int _createLimit = 1000;
    [SerializeField] float _lenght = 50;
    [SerializeField] EnemyBase _prefab = null;
    [SerializeField] Transform _root = null;

    float _cRad = 0.0f;
    Vector3 _popPos = new Vector3(0, 0, 0);

    ObjectPool<EnemyBase> _enemyPool = new ObjectPool<EnemyBase>();

    GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }
    private void Start()
    {
        _enemyPool.SetBaseObj(_prefab, _root);
        _enemyPool.SetCapacity(_createLimit);
        _enemies = _enemyPool.PoolList;
        _gameManager.OnSetEnemy += Spawn;
    }

    void Spawn()
    {
        var script = _enemyPool.Instantiate();
        if (!script)
        {
            Debug.Log($"{this.name} : <color=blue>空になりました</color>");
            return;
        }

        _cRad = Random.Range(360f, 0f);
        _popPos.x = GameManager.Player.transform.position.x + _lenght * Mathf.Cos(_cRad);
        _popPos.y = GameManager.Player.transform.position.y + _lenght * Mathf.Sin(_cRad);
        script.transform.position = _popPos;
    }
}
