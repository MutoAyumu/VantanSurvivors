using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class CSVReader : MonoBehaviour
{
    const string SHEET_ID = "138Tra7UzH6jk1h78z8vvsLjuYKJ4IIXisN1KBqYplpU";
    const string SHEET_NAME = "�V�[�g1";

    void Start()
    {
        StartCoroutine(Method(SHEET_NAME));
    }

    IEnumerator Method(string _SHEET_NAME)
    {
        UnityWebRequest request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/" + SHEET_ID + "/gviz/tq?tqx=out:csv&sheet=" + _SHEET_NAME);
        yield return request.SendWebRequest();

        switch(request.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                Debug.LogError(request.error);
                break;

            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(request.error);
                break;

            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError(request.error);
                break;

            case UnityWebRequest.Result.Success:
                List<string[]> characterDataArrayList = ConvertToArrayListFrom(request.downloadHandler.text);
                foreach (string[] characterDataArray in characterDataArrayList)
                {
                    new GameData(characterDataArray);
                }
                break;
        }
    }

    List<string[]> ConvertToArrayListFrom(string _text)
    {
        List<string[]> characterDataArrayList = new List<string[]>();
        StringReader reader = new StringReader(_text);
        reader.ReadLine();  // 1�s�ڂ̓��x���Ȃ̂ŊO��
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();        // ��s���ǂݍ���
            string[] elements = line.Split(',');    // �s�̃Z����,�ŋ�؂���
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] == "\"\"")
                {
                    continue;                       // �󔒂͏���
                }
                elements[i] = elements[i].TrimStart('"').TrimEnd('"');
            }
            characterDataArrayList.Add(elements);
        }
        return characterDataArrayList;
    }
}