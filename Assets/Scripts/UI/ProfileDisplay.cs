using UnityEngine;
using TMPro;

public class ProfileDisplay : MonoBehaviour
{
    [Header("Данные Уровней")]
    public KanaLevelCollection levelCollection; // Ссылка на ВСЕ уровни хираганы

    [Header("UI Элементы")]
    public TextMeshProUGUI hiraganaProgressText;
    public TextMeshProUGUI katakanaProgressText;

    private void Start()
    {
        UpdateProgressDisplay();
    }

    void UpdateProgressDisplay()
    {
        /*if (levelCollection == null)
        {
            Debug.LogError("Коллекция уровней не назначена!");
            if (hiraganaProgressText != null) hiraganaProgressText.text = "Хирагана: Ошибка";
            if (katakanaProgressText != null) katakanaProgressText.text = "Катакана: Ошибка";
            return;
        }*/

        if (hiraganaProgressText != null)
        {
            if (levelCollection.hiraganaLevels != null && levelCollection.hiraganaLevels.Length > 0)
            {
                int totalLevels = levelCollection.hiraganaLevels.Length;
                int unlockedCount = 0;
                for (int i = 0; i < totalLevels; i++)
                {
                    if (ProgressManager.IsLevelUnlocked(i, ModuleDisplay.KanaType.Hiragana))
                    {
                        unlockedCount++;
                    }
                }
                hiraganaProgressText.text = $"Хирагана: Пройдено {unlockedCount} / {totalLevels}";
            }
            else
            {
                hiraganaProgressText.text = "Хирагана: Нет данных";
            } 
        }
        else
        {
            Debug.LogError("UI Элемент hiraganaProgressText не назначен!");
        }

        // Прогресс Катаканы
        if (katakanaProgressText != null)
        {
            if (levelCollection.hiraganaLevels != null && levelCollection.katakanaLevels.Length > 0)
            {
                int totalLevels = levelCollection.katakanaLevels.Length;
                int unlockedCount = 0;
                for (int i = 0; i < totalLevels; i++)
                {
                    if (ProgressManager.IsLevelUnlocked(i, ModuleDisplay.KanaType.Katakana))
                    {
                        unlockedCount++;
                    }
                }
                katakanaProgressText.text = $"Катакана: Пройдено {unlockedCount} / {totalLevels}";
            }
            else
            {
                katakanaProgressText.text = "Катакана: Нет данных";
            }
        }
        else
        {
            Debug.LogError("UI Элемент hiraganaProgressText не назначен!");
        }
        /*
        // Считаем прогресс
        int totalLevels = levelCollection.hiraganaLevels.Length;
        int unlockedCount = 0;

        for(int i = 0; i < totalLevels; i++)
        {
            if (ProgressManager.IsLevelUnlocked(i))
            {
                unlockedCount++;
            }
        }
        // Отображаем
        if (unlockedCount < totalLevels)
        {
            progressText.text = $"Хирагана: Пройдено {unlockedCount - 1} / {totalLevels}";
        }
        else
        {
            progressText.text = $"Хирагана: Пройдено {unlockedCount} / {totalLevels}";
        }
        */
    }
}
