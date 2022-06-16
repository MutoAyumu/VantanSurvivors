using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DamageEffect : MonoBehaviour, IObjectPool
{
    [SerializeField] AnimationCurve _animationCurve;

    Text _text;
    RectTransform _rect;
    Vector3? _target = null;
    float _timer = 0.0f;
    Vector3 _movVec = new Vector3(0, 0.3f, 0);
    Vector3 _mov = Vector3.zero;
    Vector2 _random;

    bool _isActive;
    public bool IsActive => _isActive;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _text = GetComponent<Text>();
    }

    public void Create()
    {
        _text.enabled = true;
        _random = new Vector2(UnityEngine.Random.Range(-10.0f, 10.0f), UnityEngine.Random.Range(-5.0f, 5.0f));
        _timer = 0;
        _mov = Vector3.zero;
        _movVec = new Vector3(0, 0.3f, 0);

        GameManager.Instance.TestObjectCount(true);

        _isActive = true;
    }

    public void Create(EnemyStatus status)
    {
        
    }

    public void Destroy()
    {
        _isActive = false;
        _text.enabled = false;

        GameManager.Instance.TestObjectCount(false);
    }

    public void DisactiveForInstantiate()
    {
        _text.enabled = false;
    }
    private void Update()
    {
        if (!_isActive || Time.deltaTime <= 0) return;

        if(_target == null)
        {
            return;
        }

        _timer += Time.deltaTime;

        _mov += _movVec * _animationCurve.Evaluate(_timer);
        _rect.position = RectTransformUtility.WorldToScreenPoint(Camera.main, _target.Value) + _random;
        _rect.position += _mov;

        if(_timer >= 1f)
        {
            this.Destroy();
        }
    }

    public void Set(GameObject go, int dmg)
    {
        _target = go.transform.position;
        _text.text = "-" + dmg.ToString();
    }
    public void SetColor(Color c)
    {
        _text.color = c;
    }
}
