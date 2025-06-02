using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ModuleDisplay : MonoBehaviour
{
    public enum KanaType { Hiragana, Katakana  };

    [Header("Тип катаканы для этой карты")]
    public KanaType displayType = KanaType.Hiragana;

    [Header("Данные Уровней")]
    //public KanaModuleData[] currentLevels;
    public KanaLevelCollection levelCollection; // ССЫЛКА НА ВСЕ УРОВНИ

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

        // Проверяем, назначена ли коллекция и префаб
        if (levelCollection == null || levelCollection.hiraganaLevels == null || levelButtonPrefab == null || buttonsContainer == null)
        {
            Debug.LogError("Не назначена коллекция уровней, ее массив пуст, не назначен префаб кнопки или контейнер!");
            return;
        }

        // Получаем массив уровней из коллекции
        KanaModuleData[] levelsToDisplay;
        //KanaModuleData[] levelsToDisplay = levelCollection.hiraganaLevels;

        if (displayType == KanaType.Hiragana)
        {
            levelsToDisplay = levelCollection.hiraganaLevels;
            if(levelsToDisplay == null)
            {
                Debug.LogError("Массив уровней Хираганы в коллекции не инициализирован!");
                return;
            }
        }
        else // displayType == KanaType.Katakana
        {
            levelsToDisplay = levelCollection.katakanaLevels;
            if (levelsToDisplay == null)
            {
                Debug.LogError("Массив уровней Катаканы в коллекции не инициализирован!");
                return;
            }
        }

        for (int i = 0; i < levelsToDisplay.Length; i++)
        { 
            int levelIndex = i; // Захватываем индекс для лямбды
            KanaModuleData levelData = levelsToDisplay[i];

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
            bool isUnlocked = ProgressManager.IsLevelUnlocked(levelIndex, displayType);

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
                        SelectLevel(levelData, levelIndex, displayType);
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
    void SelectLevel(KanaModuleData selectedLevel, int levelIndex, KanaType selectedType)
    {
        if (selectedLevel == null)
        {
            Debug.LogError("Попытка выбрать null уровень!");
            return;
        }

        if(!ProgressManager.IsLevelUnlocked(levelIndex, selectedType))
        {
            Debug.LogWarning($"Попытка загрузить заблокированный уровень {selectedType} {levelIndex}.");
            return;
        }

        Debug.Log($"Выбран уровень: {selectedLevel.moduleName}");

        // Сохраняем выбранные данные в статический класс
        LevelLoadData.selectedLevelData = selectedLevel;

        // Пока что разблокируем следующий уровень сразу после выбора текущего.
        // В будущем это нужно будет перенести в конец успешного прохождения уровня.
        ProgressManager.UnlockNextLevel(levelIndex, selectedType);

        SceneManager.LoadScene("KanaLearningLevel");
    }
}
