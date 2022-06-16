using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUiAuxiliary : MonoBehaviour
{
    [SerializeField] Text _gameTime, _enemyCount, _level;
    
    private void Start()
    {
        if(!_gameTime || !_enemyCount || !_level)
        {
            Debug.Log("テキストがセットされていません");
            return;
        }

        var g = GameManager.Instance;
        var x = g.GameTimer.Value;

        _gameTime.text = ((int)(x / 60)).ToString() + ":" + ((int)(x % 60)).ToString("00");

        _enemyCount.text = g.EnemyCount.Value.ToString();

        _level.text = PlayerManager.Instance.Level.ToString();
    }
}
