using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private CardUI cardUI;

    private List<KanaCard> cardDataList = new List<KanaCard>()
    {
        new KanaCard("Карточка каны", "Знак хираганы", "あ", "a", "あか {a-ka}　- красный"),
        new KanaCard("Карточка каны", "Знак хираганы", "い", "i", "はい {ha-i}　- да!"),
        new KanaCard("Карточка каны", "Знак хираганы", "う", "u", "うち {u-chi}　- дом"),
        new KanaCard("Карточка каны", "Знак хираганы", "え", "e", "まえ {ma-e}　- перед"),
        new KanaCard("Карточка каны", "Знак хираганы", "お", "o", "かお {ka-o}　- лицо"),
    };

    private int currentCardIndex = 0;
    private GameObject currentCard;

    private void Start()
    {
        ShowCard(currentCardIndex);
    }

    // Метод для создания карточки
    public void ShowCard(int index)
    {
        if (currentCard != null) // Если уже есть карточка, удаляем её
        {
            Destroy(currentCard);
        }

        // Обновляем данные на карточке
        if (cardUI != null)
        {
            if (index >= 0 && index < cardDataList.Count)
            {
                cardUI.UpdateKanaCard(cardDataList[index]);
            }
            else
            {
                Debug.LogError("Выход за пределы списка");
            }
        }
        else
        {
            Debug.Log("CardUI не найден");
        }
    }

    // Метод для перехода к следующей карточке
    public void NextCard()
    {
        currentCardIndex = (currentCardIndex + 1) % cardDataList.Count;
        ShowCard(currentCardIndex);
    }
}
