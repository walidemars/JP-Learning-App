using UnityEngine;
using TMPro;

public class ProfileDisplay : MonoBehaviour
{
    [Header("������ �������")]
    public KanaLevelCollection hiraganaCollection; // ������ �� ��� ������ ��������

    [Header("UI ��������")]
    public TextMeshProUGUI progressText;

    private void Start()
    {
        UpdateProgressDisplay();
    }

    void UpdateProgressDisplay()
    {
        if (hiraganaCollection == null || hiraganaCollection.hiraganaLevels == null)
        {
            Debug.LogError("��������� ������� �������� �� ���������!");
            if (progressText != null) progressText.text = "������: ��� ������";
            return;
        }

        if (progressText == null)
        {
            Debug.LogError("UI ������� progressText �� ��������!");
            return;
        }

        // ������� ��������
        int totalLevels = hiraganaCollection.hiraganaLevels.Length;
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
    }
}
