using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    // Метод, который будет вызываться при нажатии кнопки "Старт"
    public void StartGame()
    {
        Debug.Log("Нажата кнопка СТАРТ!");
    }

    // Метод для кнопки "Профиль"
    public void OpenProfile()
    {
        Debug.Log("Нажата кнопка ПРОФИЛЬ!");
    }

    // Метод для кнопки "Настройки"
    public void OpenSettings()
    {
        Debug.Log("Нажата кнопка НАСТРОЙКИ!");
    }

    // Метод для кнопки "Выход"
    public void QuitApplication()
    {
        Debug.Log("Нажата кнопка ВЫХОД!");
        Application.Quit();

#if UNITY_EDITOR
        Debug.Log("Application.Quit() вызвана (в редакторе не закрывает приложение).");
#endif
    }

    private void Start()
    {
        Debug.Log("MainMenuManager запущен!");
    }
}
