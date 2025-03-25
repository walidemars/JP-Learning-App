using TMPro;
using UnityEngine;

public class CardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI wordText;
    [SerializeField] private TextMeshProUGUI translationText;
    [SerializeField] private TextMeshProUGUI exampleText;


    // ����� ��� ���������� UI
    public void UpdateKanaCard(KanaCard data)
    {
        if (data == null)
        {
            Debug.LogError("Data �� ������");
            return;
        }

        if (titleText != null)
        {
            titleText.text = data.title;
        }
        else
        {
            Debug.LogError("TitleText �� ��������");
        }

        if (descriptionText != null)
        {
            descriptionText.text = data.description;
        }
        else
        {
            Debug.LogError("DescriptionText �� ��������");
        }

        if (wordText != null)
        {
            wordText.text = data.kana;
        }
        else
        {
            Debug.LogError("WordText �� ��������");
        }

        if (translationText != null)
        {
            translationText.text = data.translation;
        }
        else
        {
            Debug.LogError("TranslationText �� ��������");
        }

        if (exampleText != null)
        {
            exampleText.text = data.example;
        }
        else
        {
            Debug.LogError("ExampleText �� ��������");
        }
    }
}
