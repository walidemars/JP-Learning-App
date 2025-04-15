using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    // Метод для вызова по нажатию кнопки "Сбросить прогресс"
    public void ResetProgress()
    {
        Debug.Log("Запрос на сброс прогресса");

        // В будущем здесь стоит добавить диалог подтверждения

        ProgressManager.ResetAllLevelProgress();
        Debug.Log("Прогресс успешно сброшен!");
    }
}
