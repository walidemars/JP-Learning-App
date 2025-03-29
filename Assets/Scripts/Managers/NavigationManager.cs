using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour
{
    // ����� ��� �������� � ������� ����
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // ����� ��� �������� �� ����� �������
    public void GoToModuleMap()
    {
        SceneManager.LoadScene("ModuleMap");
    }
}
