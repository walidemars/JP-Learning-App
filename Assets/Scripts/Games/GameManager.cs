using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Настройки игры")]
    public int scorePerCorrect = 10;
    public int initialLives = 5;
    public float gameDuration = 60f;

    [Header("UI Элементы")]
    public TextMeshProUGUI targetRomajiText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI livesText;
    public GameObject gameOverPanel;
    public Button restartButton;

    [Header("Ссылки")]
    public SpawnerManager spawnerManager;

    private string currentTargetRomaji;
    private int score = 0;
    private int lives;
    private float timeLeft;
    private bool isGameActive = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        restartButton.onClick.AddListener(RestartGame);
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        score = 0;
        lives = initialLives;
        timeLeft = gameDuration;
        isGameActive = true;

        gameOverPanel.SetActive(false);

        if (spawnerManager != null)
        {
            spawnerManager.StartSpawning();
        }
        else
        {
            Debug.LogError("SpawnerManager не назначен в GameManager!");
            isGameActive = false;
            return;
        }

        SetNewTarget();
        StartCoroutine(GameTimer());

        UpdateUI();
    }

    private void SetNewTarget()
    {
        if (spawnerManager == null) return;

        KanaCharacterData randomKana = spawnerManager.GetRandomKanaData();
        if (randomKana != null)
        {
            currentTargetRomaji = randomKana.romaji;
            targetRomajiText.text = currentTargetRomaji;
        }
        else
        {
            Debug.LogWarning("Не удалось получить новую случайную кану для цели.");
        }
    }

    private IEnumerator GameTimer()
    {
        while (timeLeft > 0 && isGameActive)
        {
            timeLeft -= Time.deltaTime;
            UpdateUI();
            yield return null;
        }

        if (isGameActive)
        {
            timeLeft = 0;
            UpdateUI();
            EndGame();
        }
    }

    public void HandleKanaClick(KanaCharacterData kanaData)
    {
        if (!isGameActive || kanaData == null) return;

        if (kanaData.romaji == currentTargetRomaji)
        {
            score++;
            if (score >= scorePerCorrect)
            {
                lives = 0;
                UpdateUI();
                EndGame();
                return;
            }
        }
        else
        {
            lives--;
            if (lives <= 0)
            {
                lives = 0;
                UpdateUI();
                EndGame();
                return;
            }
        }

        // Меняем цель после каждого клика
        SetNewTarget();
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = $"Очки: {score} / {scorePerCorrect}";
        timerText.text = $"Время: {Mathf.CeilToInt(timeLeft)}";
        livesText.text = $"Жизни: {lives}";
    }

    private void EndGame()
    {
        if (!isGameActive) return;

        isGameActive = false;
        if (spawnerManager != null)
        {
            spawnerManager.StopSpawning();
        }
        gameOverPanel.SetActive(true);
        Debug.Log("Игра окончена!");
    }

    private void RestartGame()
    {
        StartGame();
    }

    public bool IsGameActive()
    {
        return isGameActive;
    }
}
