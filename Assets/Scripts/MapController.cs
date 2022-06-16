using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    [SerializeField] Vector2 _loopSize = Vector2.zero;
    Transform _player;

    Vector2[] _points = new Vector2[2];

    private void Start()
    {
        //‰Eã
        _points[0] = new Vector2(_loopSize.x / 2, _loopSize.y / 2);
        //¶‰º
        _points[1] = new Vector2(-_loopSize.x / 2, -_loopSize.y / 2);

        _player = PlayerManager.Player.transform;
    }

    private void Update()
    {
        if(_player.position.x > _points[0].x + this.transform.position.x)
        {
            //var pos = _player.position;
            //pos = _points[1];
            //pos.x += _player.position.y;
            //_player.position = pos;
            var pos = this.transform.position;
            pos.x += _loopSize.x;
            this.transform.position = pos;

        }
        else if(_player.position.x < _points[1].x + this.transform.position.x)
        {
            //var pos = _player.position;
            //pos = _points[0];
            //pos.x -= _player.position.y;
            //_player.position = pos;
            var pos = this.transform.position;
            pos.x -= _loopSize.x;
            this.transform.position = pos;
        }

        if (_player.position.y > _points[0].y + this.transform.position.y)
        {
            //var pos = _player.position;
            //pos = _points[1];
            //pos.y += _player.position.x;
            //_player.position = pos;
            var pos = this.transform.position;
            pos.y += _loopSize.y;
            this.transform.position = pos;
        }
        else if (_player.position.y < _points[1].y + this.transform.position.y)
        {
            //var pos = _player.position;
            //pos = _points[0];
            //pos.y -= _player.position.x;
            //_player.position = pos;
            var pos = this.transform.position;
            pos.y -= _loopSize.y;
            this.transform.position = pos;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(this.transform.position, _loopSize);
    }
}
