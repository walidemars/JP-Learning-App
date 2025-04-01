using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

        /*foreach(KanaModuleData levelData in currentLevels)
        {
            // ВАЖНО: Создаем локальную копию для лямбда-выражения
            KanaModuleData currentLevelDataForButton = levelData;

            GameObject buttonGO = Instantiate(levelButtonPrefab, buttonsContainer);
            TextMeshProUGUI buttonText = buttonGO.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = currentLevelDataForButton.moduleName;
            }

            // Получаем компонент Button
            Button buttonComponent = buttonGO.GetComponentInChildren<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() =>
                {
                    // Этот код выполнится при клике на ЭТУ конкретную кнопку
                    SelectLevel(currentLevelDataForButton);
                });
            }
            else
            {
                Debug.LogWarning($"На префабе кнопки {levelButtonPrefab.name} не найден компонент Button!", buttonGO);
            }
        }*/

        for (int i = 0; i < currentLevels.Length; i++)
        { 
            int levelIndex = i; // Захватываем индекс для лямбды
            KanaModuleData levelData = currentLevels[i];

            GameObject buttonGO = Instantiate(levelButtonPrefab, buttonsContainer);
            TextMeshProUGUI buttonText = buttonGO.GetComponentInChildren<TextMeshProUGUI>();
            // Получаем компонент Button
            Button buttonComponent = buttonGO.GetComponentInChildren<Button>();

            // Устанавливаем текст кнопки
            if (buttonText != null)
            {
                buttonText.text = levelData.moduleName;
            }

            // Проверяем, разблокирован ли уровень
            bool isUnlocked = ProgressManager.IsLevelUnlocked(levelIndex);

            if (buttonComponent != null)
            {
                // Устанавливаем интерактивность кнопки
                buttonComponent.interactable = isUnlocked;

                // Изменяем внешний вид для заблокированных кнопок
                if (!isUnlocked )
                {
                    if (buttonText != null) buttonText.color = Color.gray;
                }

                // Добавляем слушателя клика только если кнопка доступна
                if (isUnlocked)
                {
                    buttonComponent.onClick.AddListener(() =>
                    {
                        // Этот код выполнится при клике на ЭТУ конкретную кнопку
                        SelectLevel(levelData, levelIndex);
                    });
                }
                
            }
            else
            {
                Debug.LogWarning($"На префабе кнопки {levelButtonPrefab.name} не найден компонент Button!", buttonGO);
            }
        }
    }

    // Mетод для обработки выбора уровня
    void SelectLevel(KanaModuleData selectedLevel, int levelIndex)
    {
        if (selectedLevel == null)
        {
            Debug.LogError("Попытка выбрать null уровень!");
            return;
        }

        if(!ProgressManager.IsLevelUnlocked(levelIndex))
        {
            Debug.LogWarning($"Попытка загрузить заблокированный уровень {levelIndex}.");
            return;
        }

        Debug.Log($"Выбран уровень: {selectedLevel.moduleName}");

        // Сохраняем выбранные данные в статический класс
        LevelLoadData.selectedLevelData = selectedLevel;

        // Пока что разблокируем следующий уровень сразу после выбора текущего.
        // В будущем это нужно будет перенести в конец успешного прохождения уровня.
        ProgressManager.UnlockNextLevel(levelIndex);

        SceneManager.LoadScene("KanaLearningLevel");
    }

    public void ResetProgress()
    {
        ProgressManager.ResetAllLevelProgress();
        // Перегенерировать кнопки, чтобы обновить их вид
        GenerateLevelButtons();
        Debug.Log("Прогресс сброшен, кнопки обновлены.");
    }
}
