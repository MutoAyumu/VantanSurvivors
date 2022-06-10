using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    Tilemap[,] _maps = new Tilemap[3,3];
    Vector3 _screenRightTop;
    Vector3 _screenLeftDown;

    float _screenWidth;
    float _screenHeight;

    int _x;
    int _y;

    [SerializeField] Transform _rightTopPos;
    [SerializeField] Transform _leftDownPos;

    private void Awake()
    {
        var t = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        _screenWidth = t.x;
        _screenHeight = t.y;

        SetUp();
        SetUpPos();
    }
    void SetUp()
    {   
        var chilleds = GetComponentsInChildren<Tilemap>();
        var c = 0;
        _x = 1;
        _y = 1;

        for(int i= 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                _maps[i, j] = chilleds[c];
                c++;
            }
        }
    }
    void SetUpPos()
    {
        _screenRightTop = _maps[_y, _x].transform.position + new Vector3(_screenWidth, _screenHeight);
        _screenLeftDown = _maps[_y, _x].transform.position + new Vector3(-_screenWidth, -_screenHeight);

        _rightTopPos.position = _screenRightTop;
        _leftDownPos.position = _screenLeftDown;
    }
    private void Update()
    {
        var playerPos = PlayerManager.Player.transform.position;

        if (playerPos.y >= _screenRightTop.y)   //è„
        {
            Debug.Log("Topà⁄ìÆ");
            _y--;

            if(_y < 0)
            {
                _y = 1;
            }

            for (int i = 0; i < 3; i++)
            {
                _maps[2 - _y, 1].transform.position = _maps[_y, 1].transform.position + new Vector3(0, _screenHeight * 2);
            }

            SetUpPos();
        }
        if (playerPos.y <= _screenLeftDown.y)   //â∫
        {
            Debug.Log("Downà⁄ìÆ");
            _y++;
            SetUpPos();
        }
        if (playerPos.x >= _screenRightTop.x)   //âE
        {
            Debug.Log("Rightà⁄ìÆ");
            _x++;
            SetUpPos();
        }
        if (playerPos.x <= _screenLeftDown.x)   //ç∂
        {
            Debug.Log("Leftà⁄ìÆ");
            _x--;
            SetUpPos();
        }
    }
}
