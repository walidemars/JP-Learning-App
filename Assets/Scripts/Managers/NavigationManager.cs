using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

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
