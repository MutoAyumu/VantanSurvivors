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

    [Header("フェーズごとに出す敵を設定する")]
    [SerializeField] PhaseEnemyList[] _phaseEnemies = default;

    [Header("敵のログ表示フラグ")]
    [SerializeField] bool _isDebugLog;

    ObjectPool<EnemyBase> _enemyPool = new ObjectPool<EnemyBase>();

    GameManager _gameManager;

    static public List<EnemyBase> Enemies { get => Instance._enemies;}
    public bool DebugLog { get => _isDebugLog;}

    protected override void OnAwake()
    {
        _gameManager = GameManager.Instance;
    }
    private void Start()
    {
        var root = new GameObject("EnemyRoot").transform;
        var prefab = Resources.Load<EnemyBase>("BaseEnemy");

        _enemyPool.SetBaseObj(prefab, root);
        _enemyPool.SetCapacity(_createLimit);
        _enemies = _enemyPool.PoolList;
        _gameManager.OnSetEnemy += Spawn;
    }

    void Spawn()
    {
        var phaseEnemy = _phaseEnemies[Mathf.Clamp(GameManager.PhaseCount, 0, _phaseEnemies.Length - 1)].Enemies;
        var length = phaseEnemy.Length;
        var status = phaseEnemy[Random.Range(0, length)];
        var script = _enemyPool.Instantiate(status);

        if (!script)
        {
            Debug.Log($"{this.name} : <color=blue>空になりました</color>");
            return;
        }

        var cRad = Random.Range(360f, 0f);
        Vector3 popPos = new Vector3(0, 0, 0);
        popPos.x = PlayerManager.Player.transform.position.x + _lenght * Mathf.Cos(cRad);
        popPos.y = PlayerManager.Player.transform.position.y + _lenght * Mathf.Sin(cRad);
        script.transform.position = popPos;
    }
}
[System.Serializable]
public class PhaseEnemyList
{
    [SerializeField] EnemyStatus[] _enemies = default;

    public EnemyStatus[] Enemies { get => _enemies;}
}
