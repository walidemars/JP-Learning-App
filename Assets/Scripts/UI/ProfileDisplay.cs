using UnityEngine;
using TMPro;

public class ProfileDisplay : MonoBehaviour
{
    [Header("������ �������")]
    public KanaLevelCollection levelCollection; // ������ �� ��� ������ ��������

    [Header("UI ��������")]
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
            Debug.LogError("��������� ������� �� ���������!");
            if (hiraganaProgressText != null) hiraganaProgressText.text = "��������: ������";
            if (katakanaProgressText != null) katakanaProgressText.text = "��������: ������";
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
                hiraganaProgressText.text = $"��������: �������� {unlockedCount} / {totalLevels}";
            }
            else
            {
                hiraganaProgressText.text = "��������: ��� ������";
            } 
        }
        else
        {
            Debug.LogError("UI ������� hiraganaProgressText �� ��������!");
        }

        // �������� ��������
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
                katakanaProgressText.text = $"��������: �������� {unlockedCount} / {totalLevels}";
            }
            else
            {
                katakanaProgressText.text = "��������: ��� ������";
            }
        }
        else
        {
            Debug.LogError("UI ������� hiraganaProgressText �� ��������!");
        }
        /*
        // ������� ��������
        int totalLevels = levelCollection.hiraganaLevels.Length;
        int unlockedCount = 0;

        for(int i = 0; i < totalLevels; i++)
        {
            if (ProgressManager.IsLevelUnlocked(i))
            {
                unlockedCount++;
            }
        }
        // ����������
        if (unlockedCount < totalLevels)
        {
            progressText.text = $"��������: �������� {unlockedCount - 1} / {totalLevels}";
        }
        else
        {
            progressText.text = $"��������: �������� {unlockedCount} / {totalLevels}";
        }
        */
    }
}
