using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    // �����, ������� ����� ���������� ��� ������� ������ "�����"
    public void StartGame()
    {
        Debug.Log("������ ������ �����!");
    }

    // ����� ��� ������ "�������"
    public void OpenProfile()
    {
        Debug.Log("������ ������ �������!");
    }

    // ����� ��� ������ "���������"
    public void OpenSettings()
    {
        Debug.Log("������ ������ ���������!");
    }

    // ����� ��� ������ "�����"
    public void QuitApplication()
    {
        Debug.Log("������ ������ �����!");
        Application.Quit();

#if UNITY_EDITOR
        Debug.Log("Application.Quit() ������� (� ��������� �� ��������� ����������).");
#endif
    }

    private void Start()
    {
        Debug.Log("MainMenuManager �������!");
    }
}
