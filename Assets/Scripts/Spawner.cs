using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("�����Ɋւ�����")]
    [SerializeField, Tooltip("Start���ɍ쐬����G�̐�")] int _createLimit = 1000;
    [SerializeField, Tooltip("�����n�_�̃v���C���[����̋���")] float _lenght = 50;

    [Header("�t�F�[�Y���Ƃɏo���G��ݒ肷��")]
    [SerializeField] PhaseEnemyList[] _phaseEnemies = default;

    [Header("�t�F�[�Y���ƂɓG�𐶐����鎞��")]
    [SerializeField] float[] _spawnTimes;
    Timer _spawneTimer = new Timer();

    [Header("���̃t�F�[�Y�ɑJ��܂ł̎���")]
    [SerializeField] float _phaseTime = 20f;
    Timer _phaseTimer = new Timer();

    [Header("�G�̃��O�\���t���O")]
    [SerializeField] bool _isDebugLog;

    ObjectPool<EnemyBase> _enemyPool = new ObjectPool<EnemyBase>();

    public event System.Action OnPhaseCallback;

    GameManager _gameManager;

    public float[] SpawnTimes { get => _spawnTimes;}
    public float PhaseTime { get => _phaseTime;}

    private void Start()
    {
        _gameManager = GameManager.Instance;

        var root = new GameObject("EnemyRoot").transform;
        var prefab = Resources.Load<EnemyBase>("BaseEnemy");

        _enemyPool.SetBaseObj(prefab, root);
        _enemyPool.SetCapacity(_createLimit);

        _gameManager.SetUp();
        _gameManager.SetEnemyFlag(_isDebugLog);
        _gameManager.SetPhaseListener(this);

        _phaseTimer.Setup(_phaseTime);
    }
    private void Update()
    {
        if(_spawneTimer.RunTimer())
        {
            Spawn();
        }

        if(_phaseTimer.RunTimer())
        {
            var count = GameManager.PhaseCount;

            if (count < _spawnTimes.Length - 1)
            {
                _spawneTimer.Setup(_spawnTimes[count]);
                OnPhaseCallback?.Invoke();
                Debug.Log($"{this} : <color=red>�t�F�[�Y{count}</color>");
            }
        }
    }

    void Spawn()
    {
        var phaseEnemy = _phaseEnemies[Mathf.Clamp(GameManager.PhaseCount, 0, _phaseEnemies.Length - 1)].Enemies;
        var length = phaseEnemy.Length;
        var status = phaseEnemy[Random.Range(0, length)];
        var script = _enemyPool.Instantiate(status);

        if (!script)
        {
            Debug.Log($"{this} : <color=blue>��ɂȂ�܂���</color>");
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

    public EnemyStatus[] Enemies { get => _enemies; }
}
