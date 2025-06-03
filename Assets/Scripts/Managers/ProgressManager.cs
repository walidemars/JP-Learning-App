using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public static class ProgressManager
{

    // ������� ��� ������ �������, ����� �������� ���������� ����
    //private const string LevelKeyPrefix = "Level_Unlocked_";
    public const string HiraganaPrefix = "H_Level_Unlocked_";
    public const string KatakanaPrefix = "K_Level_Unlocked_";


    // ��������������� ����� ��� ��������� �����
    private static string GetLevelKey(int levelIndex, ModuleDisplay.KanaType kanaType)
    {
        string prefix = (kanaType == ModuleDisplay.KanaType.Hiragana) ? HiraganaPrefix : KatakanaPrefix;
        return prefix + levelIndex;
    }

    // ����� ��� ��������, ������������� �� �������
    public static bool IsLevelUnlocked(int levelIndex, ModuleDisplay.KanaType kanaType)
    {
        // ������ ������� ������ �������������
        if (levelIndex == 0)
        {
            return true;
        }
        string levelKey = GetLevelKey(levelIndex, kanaType);
        return PlayerPrefs.GetInt(levelKey, 0) == 1;
    }

    // ����� ��� ������������� ������
    public static void UnlockLevel(int levelIndexToUnlock, ModuleDisplay.KanaType kanaType)
    {
        if (levelIndexToUnlock == 0 && kanaType == ModuleDisplay.KanaType.Hiragana && PlayerPrefs.GetInt(GetLevelKey(0, kanaType), 0) == 0) return;

        string levelKey = GetLevelKey(levelIndexToUnlock, kanaType);
        PlayerPrefs.SetInt(levelKey, 1);
        PlayerPrefs.Save();
        Debug.Log($"������� {levelIndexToUnlock + 1} �������������! ����: {levelKey}");
    }

    public static void UnlockNextLevel(int completeLevelIndex, ModuleDisplay.KanaType kanaType)
    {
        UnlockLevel(completeLevelIndex + 1, kanaType);
    }

    // ����� ��� ������ ����� ��������� �� �������
    public static void ResetAllLevelProgress()
    {
        Debug.Log("����� ��������� ������� ��� �������� � ��������.");
        List<string> keysToDelete = new List<string>();

        for (int i = 1; i < 100; i++)
        {
            string HlevelKey = GetLevelKey(i, ModuleDisplay.KanaType.Hiragana);
            if (PlayerPrefs.HasKey(HlevelKey))
            {
                keysToDelete.Add(HlevelKey);
            }

            string KlevelKey = GetLevelKey(i, ModuleDisplay.KanaType.Katakana);
            if (PlayerPrefs.HasKey(KlevelKey))
            {
                keysToDelete.Add(KlevelKey);
            }
        }
        // PlayerPrefs.DeleteAll();

        Debug.Log($"������� ������ ��� ��������: {keysToDelete.Count}");

        foreach (string key in keysToDelete)
        {
            PlayerPrefs.DeleteKey(key);
        }

        PlayerPrefs.Save();
        Debug.Log("����� ��������� ��������.");
    }
}
