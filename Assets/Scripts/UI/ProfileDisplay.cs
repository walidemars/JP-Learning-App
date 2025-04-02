using UnityEngine;
using TMPro;

public class ProfileDisplay : MonoBehaviour
{
    [Header("Данные Уровней")]
    public KanaLevelCollection hiraganaCollection; // Ссылка на ВСЕ уровни хираганы

    [Header("UI Элементы")]
    public TextMeshProUGUI progressText;

    private void Start()
    {
        UpdateProgressDisplay();
    }

    void UpdateProgressDisplay()
    {
        if (hiraganaCollection == null || hiraganaCollection.hiraganaLevels == null)
        {
            Debug.LogError("Коллекция уровней хираганы не назначена!");
            if (progressText != null) progressText.text = "Ошибка: нет данных";
            return;
        }

        if (progressText == null)
        {
            Debug.LogError("UI элемент progressText не назначен!");
            return;
        }

        // Считаем прогресс
        int totalLevels = hiraganaCollection.hiraganaLevels.Length;
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
    }
}
