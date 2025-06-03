using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class KanaDisplay : MonoBehaviour
{
    [Header("Данные")]
    //public KanaCharacterData currentData;
    private KanaModuleData loadedLevelData;
    private int currentKanaIndex = 0;

    [Header("UI Элементы")]
    public TextMeshProUGUI kanaTextUI;
    public TextMeshProUGUI romajiTextUI;
    public Button previousButton;
    public Button nextButton;

    [Header("Панели Режимов")]
    public GameObject studyPanel;
    public GameObject quizPanel;

    [Header("UI Элементы Викторины")]
    public TextMeshProUGUI quizKanaText;
    public TextMeshProUGUI resultText;
    public Button[] answerButtons;
    public Button quitQuizButton;
    public TextMeshProUGUI scoreText;

    [Header("Настройки Викторины")]
    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;
    public float delayToShowNext = 1.5f;

    // Состояние викторины
    private int currentQuizIndex = 0;
    private string currentCorrectRomaji = "";
    private List<KanaCharacterData> quizKanaList;
    private int score = 0;

    private void Awake()
    {
        loadedLevelData = LevelLoadData.selectedLevelData;

        if (loadedLevelData != null && loadedLevelData.kanaList != null && loadedLevelData.kanaList.Count > 0)
        {
            Debug.Log($"Загружен уровень: {loadedLevelData.moduleName} с {loadedLevelData.kanaList.Count} символами.");
            currentKanaIndex = 0;
            DisplayCurrentKana(); 
            SwitchToStudyMode();
        }
        else
        {
            Debug.LogError("Не удалось загрузить данные уровня из LevelLoadData!");
            UpdateNavigationButtons();
            SwitchToStudyMode();
        }
    }

    void DisplayCurrentKana()
    {
        if (loadedLevelData != null && loadedLevelData.kanaList != null
            && currentKanaIndex >= 0 && currentKanaIndex < loadedLevelData.kanaList.Count)
        {
            KanaCharacterData dataToShow = loadedLevelData.kanaList[currentKanaIndex];

            if (dataToShow != null)
            {
                if (kanaTextUI != null)
                {
                    kanaTextUI.text = dataToShow.kanaSymbol;
                }
                else
                {
                    Debug.LogError("UI элемент для отображения Каны не назначен в инспекторе!");
                }

                if (romajiTextUI != null)
                {
                    romajiTextUI.text = dataToShow.romaji;
                }
                else
                {
                    Debug.LogError("UI элемент для отображения Ромадзи не назначен в инспекторе!");
                }
            }
            else
            {
                Debug.LogError($"Данные для символа с индексом {currentKanaIndex} в уровне {loadedLevelData.moduleName} равны null!");
            }
        }
        else
        {
            Debug.LogError("Невозможно отобразить кану: данные уровня не загружены или индекс неверен.");
        }
        UpdateNavigationButtons();
    }

    void UpdateNavigationButtons()
    {
        bool hasData = loadedLevelData != null && loadedLevelData.kanaList != null && loadedLevelData.kanaList.Count > 0;

        if (!hasData)
        {
            previousButton.interactable = false;
            nextButton.interactable = false;
            return;
        }

        previousButton.interactable = (currentKanaIndex > 0);

        nextButton.interactable = (currentKanaIndex < loadedLevelData.kanaList.Count - 1);
    }

    void SwitchToStudyMode()
    {
        studyPanel.SetActive(true);
        quizPanel.SetActive(false);
    }

    void SwitchToQuizMode()
    {
        studyPanel.SetActive(false);
        quizPanel.SetActive(true);
        StartQuiz();
    }

    void StartQuiz()
    {
        score = 0;
        UpdateScoreText();

        quizKanaList = loadedLevelData.kanaList.OrderBy(k => Random.value).ToList();

        currentQuizIndex = 0;
        resultText.text = "";
        ShowNextQuestion();
    }

    void ShowNextQuestion()
    {
        if (/*currentQuizIndex*/score >= quizKanaList.Count)
        {
            resultText.text = $"Викторина завершена! Ваш счет: " + score;

            //Invoke(nameof(SwitchToStudyMode), delayToShowNext * 2);
            Debug.Log("Викторина завершена.");
            SwitchToStudyMode();
            return;
        }

        resultText.text = "";

        KanaCharacterData currentKana = quizKanaList[currentQuizIndex % quizKanaList.Count];
        quizKanaText.text = currentKana.kanaSymbol;
        currentCorrectRomaji = currentKana.romaji;

        List<string> options = new List<string>
        {
            currentCorrectRomaji
        };

        List<string> allRomanjiInLevel = loadedLevelData.kanaList.Select(k => k.romaji).ToList();
        List<string> wrongOption = allRomanjiInLevel
                                .Where(r => r != currentCorrectRomaji)
                                .OrderBy(r => Random.value)
                                .Take(3)
                                .ToList();
        options.AddRange(wrongOption);

        options = options.OrderBy(o => Random.value).ToList();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].onClick.RemoveAllListeners();

            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = options[i];
            }

            string optionValue = options[i];
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(optionValue));
            
            answerButtons[i].image.color = Color.white;
            answerButtons[i].interactable = true;
        }
    }

    void OnAnswerSelected(string selectRomanji)
    {
        foreach (Button button in answerButtons)
        {
            button.interactable = false;
        }

        if(selectRomanji == currentCorrectRomaji)
        {
            resultText.text = "Правильно!";
            resultText.color = correctColor;
            score++;
            FindButtonByText(selectRomanji).image.color = correctColor;
        }
        else
        {
            resultText.text = $"Неправильно! Верно: {currentCorrectRomaji}";
            resultText.color = wrongColor;
            //score++;
            FindButtonByText(selectRomanji).image.color = wrongColor;
            FindButtonByText(currentCorrectRomaji).image.color = correctColor;
        }

        UpdateScoreText();

        currentQuizIndex++;
        Invoke(nameof(ShowNextQuestion), delayToShowNext);
    }

    Button FindButtonByText(string text)
    {
        foreach(Button button in answerButtons)
        {
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null && buttonText.text == text)
            {
                return button;
            }
        }
        return null;
    }
    void UpdateScoreText()
    {
        if(scoreText != null)
        {
            scoreText.text = $"Счет: {score} / 5";
        }
    }

    public void UI_StartQuizButton()
    {
        SwitchToQuizMode();
    }

    public void UI_QuitQuizButton()
    {
        SwitchToStudyMode();
    }

    public void ShowNextKana()
    {
        if (!studyPanel.activeInHierarchy) return;

        if (loadedLevelData != null && loadedLevelData.kanaList != null && currentKanaIndex < loadedLevelData.kanaList.Count - 1)
        {
            currentKanaIndex++;
            DisplayCurrentKana();
        }
    }

    public void ShowPreviousKana()
    {
        if (!studyPanel.activeInHierarchy) return;

        if (loadedLevelData != null && loadedLevelData.kanaList != null && currentKanaIndex > 0)
        {
            currentKanaIndex--;
            DisplayCurrentKana();
        }
    }
}
