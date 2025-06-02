using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ModuleDisplay : MonoBehaviour
{
    public enum KanaType { Hiragana, Katakana  };

    [Header("��� �������� ��� ���� �����")]
    public KanaType displayType = KanaType.Hiragana;

    [Header("������ �������")]
    //public KanaModuleData[] currentLevels;
    public KanaLevelCollection levelCollection; // ������ �� ��� ������

    [Header("UI ��������")]
    public GameObject levelButtonPrefab;
    public Transform buttonsContainer;

    void Start()
    {
        GenerateLevelButtons();
    }

    void GenerateLevelButtons()
    {
        // ������� ��������� �� ������ ������
        foreach (Transform child in buttonsContainer)
        {
            Destroy(child.gameObject);
        }

        // ���������, ��������� �� ��������� � ������
        if (levelCollection == null || levelCollection.hiraganaLevels == null || levelButtonPrefab == null || buttonsContainer == null)
        {
            Debug.LogError("�� ��������� ��������� �������, �� ������ ����, �� �������� ������ ������ ��� ���������!");
            return;
        }

        // �������� ������ ������� �� ���������
        KanaModuleData[] levelsToDisplay;
        //KanaModuleData[] levelsToDisplay = levelCollection.hiraganaLevels;

        if (displayType == KanaType.Hiragana)
        {
            levelsToDisplay = levelCollection.hiraganaLevels;
            if(levelsToDisplay == null)
            {
                Debug.LogError("������ ������� �������� � ��������� �� ���������������!");
                return;
            }
        }
        else // displayType == KanaType.Katakana
        {
            levelsToDisplay = levelCollection.katakanaLevels;
            if (levelsToDisplay == null)
            {
                Debug.LogError("������ ������� �������� � ��������� �� ���������������!");
                return;
            }
        }

        for (int i = 0; i < levelsToDisplay.Length; i++)
        { 
            int levelIndex = i; // ����������� ������ ��� ������
            KanaModuleData levelData = levelsToDisplay[i];

            GameObject buttonGO = Instantiate(levelButtonPrefab, buttonsContainer);
            TextMeshProUGUI buttonText = buttonGO.GetComponentInChildren<TextMeshProUGUI>();
            // �������� ��������� Button
            Button buttonComponent = buttonGO.GetComponentInChildren<Button>();

            // ������������� ����� ������
            if (buttonText != null)
            {
                buttonText.text = levelData.moduleName;
            }

            // ���������, ������������� �� �������
            bool isUnlocked = ProgressManager.IsLevelUnlocked(levelIndex, displayType);

            if (buttonComponent != null)
            {
                // ������������� ��������������� ������
                buttonComponent.interactable = isUnlocked;

                // �������� ������� ��� ��� ��������������� ������
                if (!isUnlocked )
                {
                    if (buttonText != null) buttonText.color = Color.gray;
                }

                // ��������� ��������� ����� ������ ���� ������ ��������
                if (isUnlocked)
                {
                    buttonComponent.onClick.AddListener(() =>
                    {
                        // ���� ��� ���������� ��� ����� �� ��� ���������� ������
                        SelectLevel(levelData, levelIndex, displayType);
                    });
                }
                
            }
            else
            {
                Debug.LogWarning($"�� ������� ������ {levelButtonPrefab.name} �� ������ ��������� Button!", buttonGO);
            }
        }
    }

    // M���� ��� ��������� ������ ������
    void SelectLevel(KanaModuleData selectedLevel, int levelIndex, KanaType selectedType)
    {
        if (selectedLevel == null)
        {
            Debug.LogError("������� ������� null �������!");
            return;
        }

        if(!ProgressManager.IsLevelUnlocked(levelIndex, selectedType))
        {
            Debug.LogWarning($"������� ��������� ��������������� ������� {selectedType} {levelIndex}.");
            return;
        }

        Debug.Log($"������ �������: {selectedLevel.moduleName}");

        // ��������� ��������� ������ � ����������� �����
        LevelLoadData.selectedLevelData = selectedLevel;

        // ���� ��� ������������ ��������� ������� ����� ����� ������ ��������.
        // � ������� ��� ����� ����� ��������� � ����� ��������� ����������� ������.
        ProgressManager.UnlockNextLevel(levelIndex, selectedType);

        SceneManager.LoadScene("KanaLearningLevel");
    }
}
