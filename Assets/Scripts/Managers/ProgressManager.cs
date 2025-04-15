using UnityEngine;
using UnityEngine.InputSystem;

public static class ProgressManager
{

    // Префикс для ключей уровней, чтобы избежать конфликтов имен
    private const string LevelKeyPrefix = "Level_Unlocked_";

    // Метод для проверки, разблокирован ли уровень
    public static bool IsLevelUnlocked(int levelIndex)
    {
        // Первый уровень всегда разблокирован
        if (levelIndex == 0)
        {
            return true;
        }

        // Формируем ключ
        string levelKey = LevelKeyPrefix + levelIndex;

        // Проверяем, есть ли ключ, возвращаем true, если есть, иначе false
        return PlayerPrefs.GetInt(levelKey, 0) == 1;
    }

    // Метод для разблокировки уровня
    public static void UnlockLevel(int levelIndexToUnlock)
    {
        if (levelIndexToUnlock == 0) return;

        string levelKey = LevelKeyPrefix + levelIndexToUnlock;
        PlayerPrefs.SetInt(levelKey, 1);
        PlayerPrefs.Save();
        Debug.Log($"Уровень {levelIndexToUnlock + 1} разблокирован! Ключ: {levelKey}");
    }
    // Дополнительно: метод для разблокировки следующего уровня
    public static void UnlockNextLevel(int completeLevelIndex)
    {
        UnlockLevel(completeLevelIndex + 1);
    }

    // Метод для сброса всего прогресса по уровням
    public static void ResetAllLevelProgress()
    {
        Debug.Log("Сброс прогресса уровней для Хираганы и Катаканы.");

        for (int i = 1; i < 100;  i++)
        {
            string levelKey = LevelKeyPrefix + i;
            if (PlayerPrefs.HasKey(LevelKeyPrefix + i))
            {
                PlayerPrefs.DeleteKey(LevelKeyPrefix + i);
                Debug.Log($"Удален ключ: {levelKey + i}");
            }
        }
        // PlayerPrefs.DeleteAll(); // Удаление ВСЕХ сохраненных данных

        PlayerPrefs.Save();
        Debug.Log("Сброс прогресса завершен.");
    }
}
