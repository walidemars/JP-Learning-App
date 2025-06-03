using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("ModuleMap");
    }

    public void OpenProfile()
    {
        SceneManager.LoadScene("ProfileScreen");
    }

    public void GoToDictionaryScreen()
    {
        SceneManager.LoadScene("DictionaryScreen");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScreen");
    }

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
