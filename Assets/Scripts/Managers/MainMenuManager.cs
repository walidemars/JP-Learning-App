using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // �����, ������� ����� ���������� ��� ������� ������ "�����"
    public void StartGame()
    {
        //Debug.Log("������ ������ �����!");
        SceneManager.LoadScene("ModuleMap");
    }

    // ����� ��� ������ "�������"
    public void OpenProfile()
    {
        //Debug.Log("������ ������ �������!");
        SceneManager.LoadScene("ProfileScreen");
    }

    // ����� ��� ������ "���������"
    public void OpenSettings()
    {
        //Debug.Log("������ ������ ���������!");
        SceneManager.LoadScene("SettingsScreen");
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
