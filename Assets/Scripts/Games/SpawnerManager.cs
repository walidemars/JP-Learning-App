using System.Collections;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [Header("Настройки спавна")]
    public float initialSpawnRate = 1f;
    public float spawnWidth = 8f;
    public float minSpawnRate = 0.3f;
    public float spawnRateDecrease = 0.05f;
    public float spawnRateDecreaseInterval = 5f;
    public Vector3 kanaSpawnScale = new Vector3(0.2f, 0.2f, 0.2f);

    [Header("Префаб и данные")]
    public GameObject kanaPrefab;
    private KanaModuleData currentLevelData;

    private Coroutine spawningCoroutine;
    private Coroutine difficultyCoroutine;
    private float currentSpawnRate; // Текущая скорость спавна

    private void Awake()
    {
        if (currentLevelData == null)
        {
            currentLevelData = LevelLoadData.selectedLevelData;
        }

        if (currentLevelData == null || currentLevelData.kanaList == null || currentLevelData.kanaList.Count == 0)
        {
            Debug.LogError("SpawnerManager: Не удалось загрузить данные уровня! Убедитесь, что LevelLoadData.selectedLevelData установлено.");
            enabled = false;
            return;
        }
        currentSpawnRate = initialSpawnRate;
    }

    public void StartSpawning()
    {
        if (!enabled) return;

        currentSpawnRate = initialSpawnRate; // Сброс скорости спавна при старте
        spawningCoroutine = StartCoroutine(SpawnKanas());
        difficultyCoroutine = StartCoroutine(IncreaseDifficulty());
    }

    public void StopSpawning()
    {
        if (spawningCoroutine != null) StopCoroutine(spawningCoroutine);
        if (difficultyCoroutine != null) StopCoroutine(difficultyCoroutine);
    }

    private IEnumerator SpawnKanas()
    {
        while (true)
        {
            if (GameManager.Instance != null && !GameManager.Instance.IsGameActive())
            {
                yield return null;
                continue;
            }

            yield return new WaitForSeconds(currentSpawnRate);
            SpawnSingleKana();
        }
    }

    private IEnumerator IncreaseDifficulty()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRateDecreaseInterval);

            if (GameManager.Instance != null && GameManager.Instance.IsGameActive())
            {
                currentSpawnRate = Mathf.Max(minSpawnRate, currentSpawnRate - spawnRateDecrease);
            }
        }
    }

    public void SpawnSingleKana()
    {
        if (kanaPrefab == null || currentLevelData == null || GameManager.Instance == null)
        {
            Debug.LogWarning("SpawnerManager: Не могу создать кану. Префаб, данные уровня или GameManager отсутствуют.");
            return;
        }

        if (Camera.main == null || !Camera.main.orthographic)
        {
            Debug.LogError("SpawnerManager: Основная ортографическая камера не найдена! Невозможно определить позицию спавна.");
            return;
        }

        float xPos = Random.Range(-spawnWidth / 2, spawnWidth / 2);

        Vector3 spawnPos = new Vector3(xPos, Camera.main.orthographicSize + 1f, 0);

        GameObject newKana = Instantiate(kanaPrefab, spawnPos, Quaternion.identity);

        newKana.transform.localScale = kanaSpawnScale; 

        KanaCharacterData randomKana = GetRandomKanaData();
        if (randomKana == null)
        {
            Debug.LogWarning("SpawnerManager: Не удалось получить случайные данные каны для спавна.");
            Destroy(newKana);
            return;
        }

        KanaFallingObject kanaObject = newKana.GetComponent<KanaFallingObject>();
        if (kanaObject != null)
        {
            // Передаем данные и коллбэк HandleKanaClick из GameManager
            kanaObject.Setup(randomKana, GameManager.Instance.HandleKanaClick);
        }
        else
        {
            Debug.LogError("SpawnerManager: Префаб каны не содержит компонент KanaFallingObject!");
            Destroy(newKana); // Уничтожаем, если компонент отсутствует
        }
    }

    public KanaCharacterData GetRandomKanaData()
    {
        if (currentLevelData == null || currentLevelData.kanaList == null || currentLevelData.kanaList.Count == 0)
        {
            return null;
        }
        return currentLevelData.kanaList[Random.Range(0, currentLevelData.kanaList.Count)];
    }
}
