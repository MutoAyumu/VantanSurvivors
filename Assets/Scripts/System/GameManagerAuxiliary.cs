using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerAuxiliary : MonoBehaviour
{
    [SerializeField] RectTransform _gameOverPanel;

    GameManager _gameManager;

    public delegate void MonoEvent();
    MonoEvent _updateCall;

    private void Start()
    {
        _gameManager = GameManager.Instance;

        _gameManager.OnGameOver += GameOver;
        _gameManager.SetGameListener(this);

        _gameOverPanel.gameObject.SetActive(false);
    }
    public void SetUpdateCallback(MonoEvent e)
    {
        _updateCall = e;
    }
    private void Update()
    {
        _updateCall?.Invoke();
    }
    void GameOver()
    {
        if(_gameOverPanel)
        {
            _gameOverPanel.gameObject.SetActive(true);
        }
    }
}
