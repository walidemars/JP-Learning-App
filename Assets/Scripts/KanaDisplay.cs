using UnityEngine;
using TMPro;

public class KanaDisplay : MonoBehaviour
{
    [Header("Данные")]
    public KanaCharacterData currentData;

    [Header("UI Элементы")]
    public TextMeshProUGUI kanaTextUI;
    public TextMeshProUGUI romajiTextUI;


    void Start()
    {
        DisplayKanaData();
    }

    void DisplayKanaData()
    {
        if (currentData != null)
        {
            if (kanaTextUI != null)
            {
                kanaTextUI.text = currentData.kanaSymbol;
            }
            else
            {
                Debug.LogError("UI элемент для отображения Каны ('kanaTextUI') не назначен в инспекторе!");
            }

            if (romajiTextUI != null)
            {
                romajiTextUI.text = currentData.romaji;
            }
            else
            {
                Debug.LogError("UI элемент для отображения Ромадзи ('romajiTextUI') не назначен в инспекторе!");
            }
        }
        else
        {
            Debug.LogError("ScriptableObject 'currentKana' не назначен в инспекторе!");
            if (kanaTextUI != null) kanaTextUI.text = "Error!";
            if (romajiTextUI != null) romajiTextUI.text = "No data assigned";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
