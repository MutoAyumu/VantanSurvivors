using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    static DamagePopup Instance;
    ObjectPool<DamageEffect> _damagePool = new ObjectPool<DamageEffect>();
    [SerializeField] Color _highColor, _lowColor;

    private void Awake()
    {
        Instance = this;

        var root = Instance.transform;
        var prefab = Resources.Load<DamageEffect>("DamageEffect");

        _damagePool.SetBaseObj(prefab, root);
        _damagePool.SetCapacity(100);
    }
    static public void Pop(GameObject go, int dmg)
    {
        var dgo = Instance._damagePool.Instantiate();
        dgo.Set(go, dmg);

        Color col = dmg >= 10 ? Instance._highColor : Instance._lowColor;

        dgo.SetColor(col);
    }
}
