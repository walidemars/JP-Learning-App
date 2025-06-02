using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class DictionaryDisplay : MonoBehaviour
{
    [Header("Данные")]
    public KanaLevelCollection kanaCollection;

    [Header("UI Настройки")]
    public Transform contentParent; // Родительский объект для записей (Content в ScrollView)
    public GameObject entryPrefab; // Префаб для одной записи словаря
    public TextMeshProUGUI titleText;
    public Button hiraganaButton; // Кнопка выбора Хираганы
    public Button katakanaButton; // Кнопка выбора Катаканы

    // Переменная для хранения текущего режима
    private enum DisplayMode { Hiragana, Katakana };
    private DisplayMode currentMode = DisplayMode.Hiragana;

    private void Start()
    {
        //PopulateDictionary();
        // Назначаем слушатели на кнопки
        if (hiraganaButton != null)
        {
            hiraganaButton.onClick.AddListener(() => SetMode(DisplayMode.Hiragana));
        }
        if (katakanaButton != null)
        {
            katakanaButton.onClick.AddListener(() => SetMode(DisplayMode.Katakana));
        }

        // Отображаем начальный режим (Хирагана)
        SetMode(DisplayMode.Hiragana);
    }

    // Метод для установки режима и обновления отображения
    void SetMode(DisplayMode newMode)
    {
        currentMode = newMode;
        PopulateDictionary();
        UpdateTitleAndButtons();
    }

    // Метод для обновления заголовка и внешнего вида кнопок
    void UpdateTitleAndButtons()
    {
        if (titleText != null)
        {
            titleText.text = $"Словарь - {currentMode}";
        }

        // Простое выделение активной кнопки
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
            Debug.LogError("Коллекция уровней кана не назначена!");
            return;
        }
        if (contentParent == null)
        {
            Debug.LogError("Контейнер 'Content' для записей словаря не назначен!");
            return;
        }
        if (entryPrefab == null)
        {
            Debug.LogError("Префаб 'entryPrefab' для записи словаря не назначен!");
            return;
        }

        // Очистка старых записей
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // Выбор источника данных
        KanaModuleData[] sourceLevels;
        if (currentMode == DisplayMode.Hiragana)
        {
            sourceLevels = kanaCollection.hiraganaLevels;
            if(sourceLevels == null || sourceLevels.Length == 0)
            {
                Debug.LogWarning("Нет данных для Хираганы в коллекции!");
                return;
            }
        }
        else // currentMode == DisplayMode.Katakana
        {
            sourceLevels = kanaCollection.katakanaLevels;
            if (sourceLevels == null || sourceLevels.Length == 0)
            {
                Debug.LogWarning("Нет данных для Катаканы в коллекции!");
                return;
            }
        }

        // Сбор всех символов
        List<KanaCharacterData> charactersToDisplay = sourceLevels
                                                    .Where(level => level != null && level.kanaList != null) 
                                                    .SelectMany(level => level.kanaList) // Используем SelectMany для "сплющивания" списков kanaList из всех уровней в один список
                                                    .ToList();

        // Создание UI элементов
        foreach(KanaCharacterData kanaData in charactersToDisplay)
        {
            if (kanaData == null) continue;

            // Создаем экземпляр префаба
            GameObject entryGO = Instantiate(entryPrefab, contentParent);

            // Находим текстовые поля в префабе
            TextMeshProUGUI kanaText = entryGO.transform.Find("Kana Text")?.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI romajiText = entryGO.transform.Find("Romaji Text")?.GetComponent<TextMeshProUGUI>();

            // Заполняем тексты данными
            if (kanaText != null)
            {
                kanaText.text = kanaData.kanaSymbol;
            }
            else
            {
                Debug.LogWarning($"Не найден 'KanaText' в префабе {entryPrefab.name} для символа {kanaData.kanaSymbol}");
            }

            if ( romajiText != null)
            {
                romajiText.text = kanaData.romaji;
            }
            else
            {
                Debug.LogWarning($"Не найден 'RomajiText' в префабе {entryPrefab.name} для символа {kanaData.kanaSymbol}");
            }
        }
    }
}
