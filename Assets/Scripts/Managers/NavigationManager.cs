using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour
{

    // Метод для возврата в главное меню
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Метод для возврата на карту модулей
    public void GoToModuleMap()
    {
        SceneManager.LoadScene("ModuleMap");
    }

    public void GoToHiraganaMap()
    {
        SceneManager.LoadScene("HiraganaMap");
    }

    public void GoToKatakanaMap()
    {
        SceneManager.LoadScene("KatakanaMap");
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
