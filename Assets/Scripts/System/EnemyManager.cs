using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    List<EnemyBase> _enemies = new List<EnemyBase>();
    Rect _rect = new Rect(0, 0, 1, 1);
    Camera _cam = default;

    static public List<EnemyBase> EnemyList { get => Instance._enemies;}

    private void Start()
    {
        _cam = Camera.main;
    }
    //private void OnEnable()
    //{
    //    Debug.Log($"{this.name} : <color=red>OnSetTargetに登録</color>");
    //    GameManager.Instance.OnSetTarget += CheckCollisionDetection;
    //}
    //public void OnDisable()
    //{
    //    Debug.Log($"{this.name} : <color=red>OnSetTargetから削除</color>");
    //    GameManager.Instance.OnSetTarget -= CheckCollisionDetection;
    //}
    public void SetList(List<EnemyBase> enemies)
    {
        _enemies = enemies;
    }
    /// <summary>
    /// 全エネミーの当たり判定チェック
    /// </summary>
    void CheckCollisionDetection()
    {
        for (int i = 0; i < EnemyList.Count; i++)
        {
            var enemy1 = EnemyList[i];
            var view1 = CheckViewPort(enemy1.transform);

            if (!view1) continue;

            for (int j = 0; j < EnemyList.Count; j++)
            {
                var enemy2 = EnemyList[j];

                if (enemy1 == enemy2) continue;

                var view2 = CheckViewPort(enemy2.transform);

                if (!view2) continue;

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
        return Vector2.Distance(t1.position, GameManager.Player.transform.position);
    }

    bool CheckViewPort(Transform target)
    {
        var viewPos = _cam.WorldToViewportPoint(target.transform.position);

        if (_rect.Contains(viewPos))
        {
            return true;
        }

        return false;
    }
}
