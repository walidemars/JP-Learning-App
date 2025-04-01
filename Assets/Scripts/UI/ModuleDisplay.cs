using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ModuleDisplay : MonoBehaviour
{
    [Header("������ ������")]
    public KanaModuleData[] currentLevels;

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

        // ���������, ���� �� ������ ������ � ������
        if(currentLevels == null || levelButtonPrefab == null || buttonsContainer == null)
        {
            Debug.LogError("�� ��������� ������ ������, ������ ������ ��� ���������!");
            return;
        }

        /*foreach(KanaModuleData levelData in currentLevels)
        {
            // �����: ������� ��������� ����� ��� ������-���������
            KanaModuleData currentLevelDataForButton = levelData;

            GameObject buttonGO = Instantiate(levelButtonPrefab, buttonsContainer);
            TextMeshProUGUI buttonText = buttonGO.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = currentLevelDataForButton.moduleName;
            }

            // �������� ��������� Button
            Button buttonComponent = buttonGO.GetComponentInChildren<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() =>
                {
                    // ���� ��� ���������� ��� ����� �� ��� ���������� ������
                    SelectLevel(currentLevelDataForButton);
                });
            }
            else
            {
                Debug.LogWarning($"�� ������� ������ {levelButtonPrefab.name} �� ������ ��������� Button!", buttonGO);
            }
        }*/

        for (int i = 0; i < currentLevels.Length; i++)
        { 
            int levelIndex = i; // ����������� ������ ��� ������
            KanaModuleData levelData = currentLevels[i];

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
            bool isUnlocked = ProgressManager.IsLevelUnlocked(levelIndex);

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
                        SelectLevel(levelData, levelIndex);
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
    void SelectLevel(KanaModuleData selectedLevel, int levelIndex)
    {
        if (selectedLevel == null)
        {
            Debug.LogError("������� ������� null �������!");
            return;
        }

        if(!ProgressManager.IsLevelUnlocked(levelIndex))
        {
            Debug.LogWarning($"������� ��������� ��������������� ������� {levelIndex}.");
            return;
        }

        Debug.Log($"������ �������: {selectedLevel.moduleName}");

        // ��������� ��������� ������ � ����������� �����
        LevelLoadData.selectedLevelData = selectedLevel;

        // ���� ��� ������������ ��������� ������� ����� ����� ������ ��������.
        // � ������� ��� ����� ����� ��������� � ����� ��������� ����������� ������.
        ProgressManager.UnlockNextLevel(levelIndex);

        SceneManager.LoadScene("KanaLearningLevel");
    }

    public void ResetProgress()
    {
        ProgressManager.ResetAllLevelProgress();
        // ���������������� ������, ����� �������� �� ���
        GenerateLevelButtons();
        Debug.Log("�������� �������, ������ ���������.");
    }
}
