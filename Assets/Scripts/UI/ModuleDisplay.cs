using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ModuleDisplay : MonoBehaviour
{
    [Header("Данные Модуля")]
    public KanaModuleData[] currentLevels;

    [Header("UI Элементы")]
    public GameObject levelButtonPrefab;
    public Transform buttonsContainer;

    void Start()
    {
        GenerateLevelButtons();
    }

    void GenerateLevelButtons()
    {
        // Очищаем контейнер от старых кнопок
        foreach (Transform child in buttonsContainer)
        {
            Destroy(child.gameObject);
        }

        // Проверяем, есть ли данные модуля и префаб
        if(currentLevels == null || levelButtonPrefab == null || buttonsContainer == null)
        {
            Debug.LogError("Не назначены данные модуля, префаб кнопки или контейнер!");
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
