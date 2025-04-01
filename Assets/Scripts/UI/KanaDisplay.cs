using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class KanaDisplay : MonoBehaviour
{
    [Header("Данные")]
    //public KanaCharacterData currentData;
    private KanaModuleData loadedLevelData; // Данные уровня, загруженные из LevelLoadData
    private int currentKanaIndex = 0; // Индекс текущего символа в списке уровня

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
    public float delayToShowNext = 1.5f; // Задержка перед следующим вопросом

    // Состояние викторины
    private int currentQuizIndex = 0;
    private string currentCorrectRomanji = "";
    private List<KanaCharacterData> quizKanaList; // Перемешанный список для вопросов
    private int score = 0;

    private void Awake()
    {
        // Загружаем данные из статического класса
        loadedLevelData = LevelLoadData.selectedLevelData;

        // LevelLoadData.selectedLevelData = null; // Можно раскомментировать, если будут проблемы

        if (loadedLevelData != null && loadedLevelData.kanaList != null && loadedLevelData.kanaList.Count > 0)
        {
            Debug.Log($"Загружен уровень: {loadedLevelData.moduleName} с {loadedLevelData.kanaList.Count} символами.");
            currentKanaIndex = 0;
            DisplayCurrentKana(); // Отображаем первый символ 
            SwitchToStudyMode(); // Устанавливаем начальный режим
        }
        else
        {
            Debug.LogError("Не удалось загрузить данные уровня из LevelLoadData!");
            // Отображаем сообщение об ошибке в UI
            if (kanaTextUI != null) kanaTextUI.text = "Error!";
            if (romajiTextUI != null) romajiTextUI.text = "No level data loaded";
            UpdateNavigationButtons(); // Обновляем кнопки даже в случае ошибки
            SwitchToStudyMode(); // Устанавливаем режим ошибки
        }
    }

    void DisplayCurrentKana()
    {
        // Проверяем, есть ли данные и не выходит ли индекс за границы
        if (loadedLevelData != null && loadedLevelData.kanaList != null
            && currentKanaIndex >= 0 && currentKanaIndex < loadedLevelData.kanaList.Count)
        {
            // Получаем данные для текущего символа
            KanaCharacterData dataToShow = loadedLevelData.kanaList[currentKanaIndex];

            if (dataToShow != null)
            {
                if (kanaTextUI != null)
                {
                    kanaTextUI.text = dataToShow.kanaSymbol;
                }
                else
                {
                    Debug.LogError("UI элемент для отображения Каны ('kanaTextUI') не назначен в инспекторе!");
                }

                if (romajiTextUI != null)
                {
                    romajiTextUI.text = dataToShow.romaji;
                }
                else
                {
                    Debug.LogError("UI элемент для отображения Ромадзи ('romajiTextUI') не назначен в инспекторе!");
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
            if (kanaTextUI != null) kanaTextUI.text = "Error!";
            if (romajiTextUI != null) romajiTextUI.text = "No data assigned";
        }

        // Обновляем состояние кнопок навигации после отображения
        UpdateNavigationButtons();
    }

    void UpdateNavigationButtons()
    {
        if (previousButton == null || nextButton == null)
        {
            Debug.LogError($"Кнопки {previousButton.name} и {nextButton.name} не назначены в инспекторе!");
            return;
        } 

        bool hasData = loadedLevelData != null && loadedLevelData.kanaList != null && loadedLevelData.kanaList.Count > 0;

        if (!hasData)
        {
            // Если данных нет, обе кнопки неактивны
            previousButton.interactable = false;
            nextButton.interactable = false;
            return;
        }

        // Кнопка "Назад" активна, если индекс больше 0
        previousButton.interactable = (currentKanaIndex > 0);

        // Кнопка "Вперед" активна, если индекс меньше последнего индекса в списке
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
        StartQuiz(); // Начинаем викторину при переключении
    }

    void StartQuiz()
    {
        if (loadedLevelData == null || loadedLevelData.kanaList == null || loadedLevelData.kanaList.Count < 4) // Проверка на мин. кол-во
        {
            Debug.LogError("Недостаточно данных для запуска викторины (нужно мин. 4 символа)");
            resultText.text = "Ошибка: Мало данных";
            SwitchToStudyMode();
            return;
        }

        score = 0; // Сбросить счет
        UpdateScoreText();

        quizKanaList = loadedLevelData.kanaList.OrderBy(k => Random.value).ToList(); // Перемешать список для вопросов

        currentQuizIndex = 0; // Начинаем с первого вопроса
        resultText.text = ""; // Очищаем текст результата
        ShowNextQuestion();
    }

    void ShowNextQuestion()
    {
        if (/*currentQuizIndex*/score >= quizKanaList.Count) // Пока количество очков не станет равным количеству кан в уровне (можно увеличить добавив переменную)
        {
            // Викторина завершена
            resultText.text = $"Викторина завершена! Ваш счет: " + score;

            //Invoke(nameof(SwitchToStudyMode), delayToShowNext * 2); // Вернуться в режим изучения
            Debug.Log("Викторина завершена.");
            // Пока просто выходим
            SwitchToStudyMode();
            return;
        }

        resultText.text = ""; // Очищаем текст результата

        // Получаем данные для текущего вопроса
        KanaCharacterData currentKana = quizKanaList[(currentQuizIndex + 1) % quizKanaList.Count]; // Получает с первого по последний, затем снова с первого
        quizKanaText.text = currentKana.kanaSymbol;
        currentCorrectRomanji = currentKana.romaji;

        // Генерация вариантов ответа
        List<string> options = new List<string>
        {
            currentCorrectRomanji // Добавляем правильный
        };

        // Получаем все ромадзи из уровня
        List<string> allRomanjiInLevel = loadedLevelData.kanaList.Select(k => k.romaji).ToList();
        // Выбираем 3 уникальных неправильных варианта
        List<string> wrongOption = allRomanjiInLevel
                                .Where(r => r != currentCorrectRomanji) // Не равны правильному
                                .OrderBy(r => Random.value) // Перемешиваем
                                .Take(3) // Берем 3
                                .ToList();
        options.AddRange(wrongOption); // Добавляем неправильные к правильному
        //Debug.Log(options.Count);

        // Перемешиваем финальный список из 4 вариантов
        options = options.OrderBy(o => Random.value).ToList();

        // Назначаем варианты кнопкам
        if(answerButtons.Length != 4 || options.Count != 4)
        {
            Debug.LogError("Ошибка: Неправильное количество кнопок или вариантов ответа!");
            return;
        }

        for (int i = 0; i < answerButtons.Length; i++)
        {
            // Убираем старые слушатели, чтобы избежать дублирования
            answerButtons[i].onClick.RemoveAllListeners();

            // Устанавливаем текст
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = options[i];
            }

            // Добавляем нового слушателя с текущим вариантом ответа
            string optionValue = options[i]; // Захватываем значение для лямбды
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(optionValue));
            
            answerButtons[i].image.color = Color.white; // Сбрасываем цвет кнопки
            answerButtons[i].interactable = true; // Включаем кнопку
        }
    }

    // Метод обработки выбора ответа
    void OnAnswerSelected(string selectRomanji)
    {
        // Выключаем все кнопки, чтобы нельзя было нажать снова
        foreach (Button button in answerButtons)
        {
            button.interactable = false;
        }

        if(selectRomanji == currentCorrectRomanji)
        {
            resultText.text = "Правильно!";
            resultText.color = correctColor;
            score++;
            FindButtonByText(selectRomanji).image.color = correctColor;
        }
        else
        {
            resultText.text = $"Неправильно! Верно: {currentCorrectRomanji}";
            resultText.color = wrongColor;
            //score++;
            FindButtonByText(selectRomanji).image.color = wrongColor;
            FindButtonByText(currentCorrectRomanji).image.color = correctColor;
        }

        UpdateScoreText();

        // Переходим к следующему вопросу через задержку
        currentQuizIndex++;
        Invoke(nameof(ShowNextQuestion), delayToShowNext);
    }

    // метод для поиска кнопки по тексту
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

    // Метод для обновления текста счета
    void UpdateScoreText()
    {
        if(scoreText != null)
        {
            scoreText.text = $"Счет: {score}";
        }
    }

    // Публичные методы для кнопок UI
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
        if (!studyPanel.activeInHierarchy) return; // Работает только в режиме изучения

        // Проверяем, есть ли данные и не выходим ли за границы
        if (loadedLevelData != null && loadedLevelData.kanaList != null && currentKanaIndex < loadedLevelData.kanaList.Count - 1)
        {
            currentKanaIndex++;
            DisplayCurrentKana(); // Отображаем новый символ и обновляем кнопки
        }
    }

    // Метод для перехода к предыдущему символу
    public void ShowPreviousKana()
    {
        if (!studyPanel.activeInHierarchy) return; // Работает только в режиме изучения

        // Проверяем, есть ли данные и не выходим ли за границы
        if (loadedLevelData != null && loadedLevelData.kanaList != null && currentKanaIndex > 0)
        {
            currentKanaIndex--;
            DisplayCurrentKana(); // Отображаем новый символ и обновляем кнопки
        }
    }
}
