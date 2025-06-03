using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public void ResetProgress()
    {
        ProgressManager.ResetAllLevelProgress();
        Debug.Log("Прогресс успешно сброшен!");
    }
}
