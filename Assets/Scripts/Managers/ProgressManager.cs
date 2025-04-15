using UnityEngine;
using UnityEngine.InputSystem;

public static class ProgressManager
{

    // ������� ��� ������ �������, ����� �������� ���������� ����
    private const string LevelKeyPrefix = "Level_Unlocked_";

    // ����� ��� ��������, ������������� �� �������
    public static bool IsLevelUnlocked(int levelIndex)
    {
        // ������ ������� ������ �������������
        if (levelIndex == 0)
        {
            return true;
        }

        // ��������� ����
        string levelKey = LevelKeyPrefix + levelIndex;

        // ���������, ���� �� ����, ���������� true, ���� ����, ����� false
        return PlayerPrefs.GetInt(levelKey, 0) == 1;
    }

    // ����� ��� ������������� ������
    public static void UnlockLevel(int levelIndexToUnlock)
    {
        if (levelIndexToUnlock == 0) return;

        string levelKey = LevelKeyPrefix + levelIndexToUnlock;
        PlayerPrefs.SetInt(levelKey, 1);
        PlayerPrefs.Save();
        Debug.Log($"������� {levelIndexToUnlock + 1} �������������! ����: {levelKey}");
    }
    // �������������: ����� ��� ������������� ���������� ������
    public static void UnlockNextLevel(int completeLevelIndex)
    {
        UnlockLevel(completeLevelIndex + 1);
    }

    // ����� ��� ������ ����� ��������� �� �������
    public static void ResetAllLevelProgress()
    {
        Debug.Log("����� ��������� ������� ��� �������� � ��������.");

        for (int i = 1; i < 100;  i++)
        {
            string levelKey = LevelKeyPrefix + i;
            if (PlayerPrefs.HasKey(LevelKeyPrefix + i))
            {
                PlayerPrefs.DeleteKey(LevelKeyPrefix + i);
                Debug.Log($"������ ����: {levelKey + i}");
            }
        }
        // PlayerPrefs.DeleteAll(); // �������� ���� ����������� ������

        PlayerPrefs.Save();
        Debug.Log("����� ��������� ��������.");
    }
}
