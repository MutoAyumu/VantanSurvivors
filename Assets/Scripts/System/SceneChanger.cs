using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] CustomButton _button;
    [SerializeField] string _sceneName = "";
    [SerializeField] Type _type;

    private void Start()
    {
        switch(_type)
        {
            case Type.Scene:
                _button.OnSetEvent(SceneLoad);
                break;

            case Type.End:
                _button.OnSetEvent(EndGame);
                break;
        }
        
    }
    void SceneLoad()
    {
        SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Single);
    }
    void EndGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    enum Type
    {
        Scene,
        End,
    }
}
