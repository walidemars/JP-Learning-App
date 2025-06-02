using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class DictionaryDisplay : MonoBehaviour
{
    [Header("������")]
    public KanaLevelCollection kanaCollection;

    [Header("UI ���������")]
    public Transform contentParent; // ������������ ������ ��� ������� (Content � ScrollView)
    public GameObject entryPrefab; // ������ ��� ����� ������ �������
    public TextMeshProUGUI titleText;
    public Button hiraganaButton; // ������ ������ ��������
    public Button katakanaButton; // ������ ������ ��������

    // ���������� ��� �������� �������� ������
    private enum DisplayMode { Hiragana, Katakana };
    private DisplayMode currentMode = DisplayMode.Hiragana;

    private void Start()
    {
        //PopulateDictionary();
        // ��������� ��������� �� ������
        if (hiraganaButton != null)
        {
            hiraganaButton.onClick.AddListener(() => SetMode(DisplayMode.Hiragana));
        }
        if (katakanaButton != null)
        {
            katakanaButton.onClick.AddListener(() => SetMode(DisplayMode.Katakana));
        }

        // ���������� ��������� ����� (��������)
        SetMode(DisplayMode.Hiragana);
    }

    // ����� ��� ��������� ������ � ���������� �����������
    void SetMode(DisplayMode newMode)
    {
        currentMode = newMode;
        PopulateDictionary();
        UpdateTitleAndButtons();
    }

    // ����� ��� ���������� ��������� � �������� ���� ������
    void UpdateTitleAndButtons()
    {
        if (titleText != null)
        {
            titleText.text = $"������� - {currentMode}";
        }

        // ������� ��������� �������� ������
        if (hiraganaButton != null)
        {
            hiraganaButton.interactable = (currentMode != DisplayMode.Hiragana);
        }
        if (katakanaButton != null)
        {
            katakanaButton.interactable = (currentMode != DisplayMode.Katakana);
        }
    }

    void PopulateDictionary()
    {
        if (kanaCollection == null)
        {
            Debug.LogError("��������� ������� ���� �� ���������!");
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

        // ����� ��������� ������
        KanaModuleData[] sourceLevels;
        if (currentMode == DisplayMode.Hiragana)
        {
            sourceLevels = kanaCollection.hiraganaLevels;
            if(sourceLevels == null || sourceLevels.Length == 0)
            {
                Debug.LogWarning("��� ������ ��� �������� � ���������!");
                return;
            }
        }
        else // currentMode == DisplayMode.Katakana
        {
            sourceLevels = kanaCollection.katakanaLevels;
            if (sourceLevels == null || sourceLevels.Length == 0)
            {
                Debug.LogWarning("��� ������ ��� �������� � ���������!");
                return;
            }
        }

        // ���� ���� ��������
        List<KanaCharacterData> charactersToDisplay = sourceLevels
                                                    .Where(level => level != null && level.kanaList != null) 
                                                    .SelectMany(level => level.kanaList) // ���������� SelectMany ��� "�����������" ������� kanaList �� ���� ������� � ���� ������
                                                    .ToList();

        // �������� UI ���������
        foreach(KanaCharacterData kanaData in charactersToDisplay)
        {
            if (kanaData == null) continue;

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
