using UnityEditor;
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

    public void GoToDictionaryScreen()
    {
        SceneManager.LoadScene("DictionaryScreen");
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
        

#if UNITY_EDITOR
        Debug.Log("Application.Quit() ������� (� ��������� �� ��������� ����������).");
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private void Start()
    {
        Debug.Log("MainMenuManager �������!");
    }
}
