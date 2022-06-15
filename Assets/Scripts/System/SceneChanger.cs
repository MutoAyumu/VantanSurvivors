using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] CustomButton _button;
    [SerializeField] string _sceneName = "";

    private void Start()
    {
        _button.OnSetEvent(SceneLoad);
    }
    public void SceneLoad()
    {
        SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Single);
    }
}
