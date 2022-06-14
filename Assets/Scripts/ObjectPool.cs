using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Unity��́A����̌^�̃I�u�W�F�N�g�v�[��
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectPool<T> where T : UnityEngine.Object, IObjectPool
{
    T BaseObj = null;
    Transform Parent = null;
    List<T> Pool = new List<T>();
    int Index = 0;

    public List<T> PoolList { get => Pool;}

    public void SetBaseObj(T obj, Transform parent)
    {
        BaseObj = obj;
        Parent = parent;
    }

    public void Pooling(T obj)
    {
        obj.DisactiveForInstantiate();
        Pool.Add(obj);
    }

    public void SetCapacity(int size)
    {
        //���ɃI�u�W�F�N�g�T�C�Y���傫���Ƃ��͍X�V���Ȃ�
        if (size < Pool.Count) return;

        for (int i = 0; i < size; i++)
        {
            T Obj = default(T);
            if (Parent)
            {
                Obj = GameObject.Instantiate(BaseObj, Parent);
            }
            else
            {
                Obj = GameObject.Instantiate(BaseObj);
            }
            Pooling(Obj);
        }

        Debug.Log($"<color=cyan>{this.BaseObj.name}</color> : {size}�����I��");
    }

    public T Instantiate()
    {
        for (int i = 0; i < Pool.Count; i++)
        {
            int index = (Index + i) % Pool.Count;
            if (Pool[index].IsActive) continue;

            Pool[index].Create();
            return Pool[index];
        }

        T Obj = default(T);
        if (Parent)
        {
            Obj = GameObject.Instantiate(BaseObj, Parent);
        }
        else
        {
            Obj = GameObject.Instantiate(BaseObj);
        }
        Pooling(Obj);
        Obj.Create();
        return Obj;
    }
    public T Instantiate(EnemyStatus status)
    {
        for (int i = 0; i < Pool.Count; i++)
        {
            int index = (Index + i) % Pool.Count;
            if (Pool[index].IsActive) continue;

            Pool[index].Create(status);
            return Pool[index];
        }

        T Obj = default(T);
        if (Parent)
        {
            Obj = GameObject.Instantiate(BaseObj, Parent);
        }
        else
        {
            Obj = GameObject.Instantiate(BaseObj);
        }
        Pooling(Obj);
        Obj.Create();
        return Obj;
    }
}