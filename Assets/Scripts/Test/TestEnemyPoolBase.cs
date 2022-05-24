using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyPoolBase : MonoBehaviour
{
    [SerializeField] EnemyBase _enemy = default;
    [SerializeField] int _createLimit = 10;

    List<EnemyBase> _enemyList;

    private void Awake()
    {
        CreatePool();
    }
    private void OnEnable()
    {
        //AGameManager.Instance.OnSetTarget += CheckCollisionDetection;
    }
    public void OnDisable()
    {
        
    }

    void CreatePool()
    {
        _enemyList = new List<EnemyBase>();

        for (int i = 0; i < _createLimit; i++)
        {
            var e = CreateEnemy();
            _enemyList.Add(e);
        }
    }
    EnemyBase CreateEnemy()
    {
        return Instantiate(_enemy, this.transform.position + new Vector3(Random.Range(-3f,3f), this.transform.position.y), Quaternion.identity, this.transform);
    }
    public EnemyBase GetEnemy()
    {
        foreach (var obj in _enemyList)
        {
            if (!obj.gameObject.activeSelf)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        var e = CreateEnemy();
        _enemyList.Add(e);

        return e;
    }

    /// <summary>
    /// 全エネミーの当たり判定チェック
    /// </summary>
    void CheckCollisionDetection()
    {
        for (int i = 0; i < _enemyList.Count; i++)
        {
            for (int j = 0; j < _enemyList.Count; j++)
            {
                var enemy1 = _enemyList[i];
                var enemy2 = _enemyList[j];

                if (enemy1 == enemy2)
                {
                    continue;
                }

                var dir = enemy2.transform.position - enemy1.transform.position;

                var normal = dir.normalized;
                var magnitude = dir.magnitude;

                if (dir.normalized == Vector3.zero)
                {
                    normal = Vector2.up;
                }

                if (magnitude < enemy1.Radius * 2)
                {
                    var a = PlayerDistance(enemy1.transform);
                    var b = PlayerDistance(enemy2.transform);

                    if (a > b)
                    {
                        enemy2.transform.position += (enemy2.Radius * 2 - magnitude) * normal / 2;
                    }
                    else
                    {
                        enemy1.transform.position += (enemy2.Radius * 2 - magnitude) * normal / 2;
                    }
                }
            }
        }
    }
    float PlayerDistance(Transform t1)
    {
        return Vector2.Distance(t1.position, PlayerManager.Player.transform.position);
    }
}
