using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SkillSelectUI : MonoBehaviour
{
    [SerializeField] List<GameObject> _selectList = new List<GameObject>();
    List<AbilityTable> _skillTable = new List<AbilityTable>();
    List<Text> _textList = new List<Text>();

    PlayerManager _playerManager;
    VerticalLayoutGroup _selectPanel;

    private void Awake()
    {
        _playerManager = PlayerManager.Instance;
        _selectPanel = GetComponentInChildren<VerticalLayoutGroup>();
    }
    private void Start()
    {
        for(int i = 0; i < _selectList.Count; i++)
        {
            _skillTable.Add(null); //初期化
            _textList.Add(_selectList[i].GetComponentInChildren<Text>());

            var b = _selectList[i].GetComponentInChildren<CustomButton>();

            b.OnSetEvent(OnClick, i);  //カスタムボタンに関数を登録
        }

        SetActiveButton(false);
    }
    public void SelectEvent()
    {
        SetActiveButton(true);
        var list = GameData.AbilitySelectTable.Where(s => _playerManager.Level >= s.Level && s.Level <= 5);
        var total = list.Sum(s => s.Probability);
        var random = Random.Range(0, total);

        for(int i = 0; i < _selectList.Count; i++)  //初期化
        {
            _textList[i].text = "";
            _skillTable[i] = null;
        }

        for(int i = 0; i < _selectList.Count; i++)
        {
            foreach(var s in list)
            {
                if (random < s.Probability)
                {
                    _skillTable[i] = s;
                    _textList[i].text = s.Name;
                    Debug.Log(s.Name);
                    list = list.Where(k => !(k.Type == s.Type && k.Id == s.Id));    //一回出たやつを除外
                    break;
                }

                random -= s.Probability;
            }
        }
    }
    public void OnClick(int i)
    {
        _playerManager.LevelUpSelect(_skillTable[i]);
        _skillTable[i].Level++;
        SetActiveButton(false);
    }
    void SetActiveButton(bool flag)
    {
        _selectPanel.gameObject.SetActive(flag);
    }
}
