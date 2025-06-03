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
        Debug.Log("Нажата кнопка ВЫХОД!");
        

#if UNITY_EDITOR
        Debug.Log("Application.Quit() вызвана (в редакторе не закрывает приложение).");
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private void Start()
    {
        Debug.Log("MainMenuManager запущен!");
    }
}
