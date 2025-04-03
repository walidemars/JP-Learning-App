using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;            

public class DictionaryDisplay : MonoBehaviour
{
    [Header("������")]
    public KanaLevelCollection hiraganaCollection;

    [Header("UI ���������")]
    public Transform contentParent; // ������������ ������ ��� ������� (Content � ScrollView)
    public GameObject entryPrefab; // ������ ��� ����� ������ �������

    private void Start()
    {
        PopulateDictionary();
    }

    void PopulateDictionary()
    {
        if (hiraganaCollection == null || hiraganaCollection.hiraganaLevels == null)
        {
            Debug.LogError("��������� ������� �������� �� ���������!");
            return;
        }
        if (contentParent == null)
        {
            Debug.LogError("��������� 'Content' ��� ������� ������� �� ��������!");
            return;
        }
        if (entryPrefab == null)
        {
            Debug.LogError("������ 'entryPrefab' ��� ������ ������� �� ��������!");
            return;
        }

        // ������� ������ �������
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // ���� ���� ��������
        List<KanaCharacterData> allKanaCharacters = hiraganaCollection.hiraganaLevels 
                                                    .SelectMany(level => level.kanaList) // ���������� SelectMany ��� "�����������" ������� kanaList �� ���� ������� � ���� ������
                                                    .ToList();

        // �������� UI ���������
        foreach(KanaCharacterData kanaData in allKanaCharacters)
        {
            // ������� ��������� �������
            GameObject entryGO = Instantiate(entryPrefab, contentParent);

            // ������� ��������� ���� � �������
            TextMeshProUGUI kanaText = entryGO.transform.Find("Kana Text")?.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI romajiText = entryGO.transform.Find("Romaji Text")?.GetComponent<TextMeshProUGUI>();

            // ��������� ������ �������
            if (kanaText != null)
            {
                kanaText.text = kanaData.kanaSymbol;
            }
            else
            {
                Debug.LogWarning($"�� ������ 'KanaText' � ������� {entryPrefab.name} ��� ������� {kanaData.kanaSymbol}");
            }

            if ( romajiText != null)
            {
                romajiText.text = kanaData.romaji;
            }
            else
            {
                Debug.LogWarning($"�� ������ 'RomajiText' � ������� {entryPrefab.name} ��� ������� {kanaData.kanaSymbol}");
            }
        }
    }
}
