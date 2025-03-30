using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

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

        for (int i = 0; i < currentLevels.Length; i++)
        {
            GameObject buttonGO = Instantiate(levelButtonPrefab, buttonsContainer);
            TextMeshProUGUI buttonText = buttonGO.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = currentLevels[i].moduleName;
            }
        }
    }
}
