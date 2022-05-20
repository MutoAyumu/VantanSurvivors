using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 1f;

    PlayerManager _playerManager;

    private void Start()
    {
        GameManager.Instance.SetPlayer(this);
        _playerManager = PlayerManager.Instance;
    }
    private void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(h * _speed * Time.deltaTime, v * _speed * Time.deltaTime, 0);

        _playerManager.Skill.ForEach(s => s.Update());
    }
}
