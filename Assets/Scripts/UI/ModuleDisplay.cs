using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ModuleDisplay : MonoBehaviour
{
    public enum KanaType { Hiragana, Katakana  };

    [Header("Тип катаканы для этой карты")]
    public KanaType displayType = KanaType.Hiragana;

    [Header("Данные Уровней")]
    //public KanaModuleData[] currentLevels;
    public KanaLevelCollection levelCollection; // ССЫЛКА НА ВСЕ УРОВНИ

    [Header("UI Элементы")]
    public GameObject levelButtonPrefab;
    public Transform buttonsContainer;

    void Start()
    {
        GenerateLevelButtons();
    }

    void GenerateLevelButtons()
    {
        foreach (Transform child in buttonsContainer)
        {
            Destroy(child.gameObject);
        }
        KanaModuleData[] levelsToDisplay;
        //KanaModuleData[] levelsToDisplay = levelCollection.hiraganaLevels;

        if (displayType == KanaType.Hiragana)
        {
            levelsToDisplay = levelCollection.hiraganaLevels;
        }
        else // displayType == KanaType.Katakana
        {
            levelsToDisplay = levelCollection.katakanaLevels;
        }

        for (int i = 0; i < levelsToDisplay.Length; i++)
        { 
            int levelIndex = i;
            KanaModuleData levelData = levelsToDisplay[i];

            GameObject buttonGO = Instantiate(levelButtonPrefab, buttonsContainer);
            TextMeshProUGUI buttonText = buttonGO.GetComponentInChildren<TextMeshProUGUI>();
            Button buttonComponent = buttonGO.GetComponentInChildren<Button>();

            if (buttonText != null)
            {
                buttonText.text = levelData.moduleName;
            }

            bool isUnlocked = ProgressManager.IsLevelUnlocked(levelIndex, displayType);

            if (buttonComponent != null)
            {
                buttonComponent.interactable = isUnlocked;

                if (!isUnlocked )
                {
                    if (buttonText != null) buttonText.color = Color.gray;
                }

                if (isUnlocked)
                {
                    buttonComponent.onClick.AddListener(() =>
                    {
                        SelectLevel(levelData, levelIndex, displayType);
                    });
                }  
            }
            else
            {
                Debug.LogWarning($"На префабе кнопки {levelButtonPrefab.name} не найден компонент Button!", buttonGO);
            }
        }
    }

    void SelectLevel(KanaModuleData selectedLevel, int levelIndex, KanaType selectedType)
    {
        Debug.Log($"Выбран уровень: {selectedLevel.moduleName}");

        LevelLoadData.selectedLevelData = selectedLevel;

        // Пока что разблокируем следующий уровень сразу после выбора текущего.
        // В будущем это нужно будет перенести в конец успешного прохождения уровня.
        ProgressManager.UnlockNextLevel(levelIndex, selectedType);

        SceneManager.LoadScene("KanaLearningLevel");
    }
}
