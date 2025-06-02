using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("��������� ����")]
    public int scorePerCorrect = 10;
    public int initialLives = 5;
    public float gameDuration = 60f;

    [Header("UI ��������")]
    public TextMeshProUGUI targetRomajiText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI livesText;
    public GameObject gameOverPanel;
    public Button restartButton;

    [Header("������")]
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
            Debug.LogError("SpawnerManager �� �������� � GameManager!");
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
            Debug.LogWarning("�� ������� �������� ����� ��������� ���� ��� ����.");
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

        // ������ ���� ����� ������� �����
        SetNewTarget();
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = $"����: {score} / {scorePerCorrect}";
        timerText.text = $"�����: {Mathf.CeilToInt(timeLeft)}";
        livesText.text = $"�����: {lives}";
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
        Debug.Log("���� ��������!");
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
