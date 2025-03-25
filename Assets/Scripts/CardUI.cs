using TMPro;
using UnityEngine;

public class CardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI wordText;
    [SerializeField] private TextMeshProUGUI translationText;
    [SerializeField] private TextMeshProUGUI exampleText;


    // Метод для обновления UI
    public void UpdateKanaCard(KanaCard data)
    {
        if (data == null)
        {
            Debug.LogError("Data не найден");
            return;
        }

        if (titleText != null)
        {
            titleText.text = data.title;
        }
        else
        {
            Debug.LogError("TitleText не назначен");
        }

        if (descriptionText != null)
        {
            descriptionText.text = data.description;
        }
        else
        {
            Debug.LogError("DescriptionText не назначен");
        }

        if (wordText != null)
        {
            wordText.text = data.kana;
        }
        else
        {
            Debug.LogError("WordText не назначен");
        }

        if (translationText != null)
        {
            translationText.text = data.translation;
        }
        else
        {
            Debug.LogError("TranslationText не назначен");
        }

        if (exampleText != null)
        {
            exampleText.text = data.example;
        }
        else
        {
            Debug.LogError("ExampleText не назначен");
        }
    }
}
