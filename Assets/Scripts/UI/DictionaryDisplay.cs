using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;            

public class DictionaryDisplay : MonoBehaviour
{
    [Header("Данные")]
    public KanaLevelCollection hiraganaCollection;

    [Header("UI Настройки")]
    public Transform contentParent; // Родительский объект для записей (Content в ScrollView)
    public GameObject entryPrefab; // Префаб для одной записи словаря

    private void Start()
    {
        PopulateDictionary();
    }

    void PopulateDictionary()
    {
        if (hiraganaCollection == null || hiraganaCollection.hiraganaLevels == null)
        {
            Debug.LogError("Коллекция уровней хираганы не назначена!");
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

        // Сбор всех символов
        List<KanaCharacterData> allKanaCharacters = hiraganaCollection.hiraganaLevels 
                                                    .SelectMany(level => level.kanaList) // Используем SelectMany для "сплющивания" списков kanaList из всех уровней в один список
                                                    .ToList();

        // Создание UI элементов
        foreach(KanaCharacterData kanaData in allKanaCharacters)
        {
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
