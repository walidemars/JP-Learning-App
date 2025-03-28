using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private CardUI cardObj;
    [SerializeField] private Transform cardParent;

    private List<KanaCard> cardDataList = new List<KanaCard>();
    private int currentCardIndex = 0;
    private string filePath;

    private void Start()
    {
        filePath = Path.Combine(Application.streamingAssetsPath, "kana_cards.json");
        LoadCardData();

        if (cardDataList.Count > 0)
        {
            ShowCard(currentCardIndex);
        }
        else
        {
            Debug.LogWarning("Список карточек пуст.");
        }
    }

    private void LoadCardData()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError($"Файл не найден: {filePath}");
            return;
        }

        string json = File.ReadAllText(filePath);
        if (string.IsNullOrEmpty(json))
        {
            Debug.LogError("JSON файл пуст.");
            return;
        }

        try
        {
            Wrapper<KanaCard> wrapper = JsonUtility.FromJson<Wrapper<KanaCard>>(json);
            if (wrapper == null || wrapper.Items == null)
            {
                Debug.LogError("Ошибка десериализации: wrapper или Items null.");
                return;
            }

            cardDataList = new List<KanaCard>(wrapper.Items);
            Debug.Log($"Загружено карточек: {cardDataList.Count}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Ошибка при десериализации: {e.Message}");
        }
    }

    public void ShowCard(int index)
    {
        if (index < 0 || index >= cardDataList.Count)
        {
            Debug.LogError($"Неверный индекс: {index}");
            return;
        }

        if (cardObj == null || cardParent == null)
        {
            Debug.LogError("Префаб или родитель не назначены.");
            return;
        }

        cardObj.UpdateKanaCard(cardDataList[index]);
    }

    public void NextCard()
    {
        if (cardDataList.Count == 0)
        {
            Debug.LogWarning("Список карточек пуст.");
            return;
        }

        currentCardIndex = (currentCardIndex + 1) % cardDataList.Count;
        ShowCard(currentCardIndex);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}