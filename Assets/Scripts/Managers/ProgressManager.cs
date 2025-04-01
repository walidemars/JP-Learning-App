using UnityEngine;

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
    public static void UnlockLevel(int levelIndex)
    {
        if (levelIndex == 0) return;

        string levelKey = LevelKeyPrefix + levelIndex;
        PlayerPrefs.SetInt(levelKey, 1);
        PlayerPrefs.Save();
        Debug.Log($"������� {levelIndex + 1} �������������!");
    }
    // �������������: ����� ��� ������������� ���������� ������
    public static void UnlockNextLevel(int completeLevelIndex)
    {
        UnlockLevel(completeLevelIndex + 1);
    }

    // ����� ��� ������ ����� ��������� �� �������
    public static void ResetAllLevelProgress()
    {
        Debug.Log("����� ����� ��������� �������.");


        for (int i = 1; i < 100;  i++)
        {
            string levelKey = LevelKeyPrefix + i;
            if (PlayerPrefs.HasKey(levelKey))
            {
                PlayerPrefs.DeleteKey(levelKey);
                Debug.Log($"������ ����: {levelKey}");
            }
        }
        // PlayerPrefs.DeleteAll(); // �������� ���� ����������� ������

        PlayerPrefs.Save();
        Debug.Log("����� ��������� ��������.");
    }
}
