using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    // ����� ��� ������ �� ������� ������ "�������� ��������"
    public void ResetProgress()
    {
        Debug.Log("������ �� ����� ���������");

        // � ������� ����� ����� �������� ������ �������������

        ProgressManager.ResetAllLevelProgress();
        Debug.Log("�������� ������� �������!");
    }
}
