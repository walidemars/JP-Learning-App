using UnityEngine;
using TMPro;

public class KanaDisplay : MonoBehaviour
{
    [Header("������")]
    public KanaCharacterData currentData;

    [Header("UI ��������")]
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
                Debug.LogError("UI ������� ��� ����������� ���� ('kanaTextUI') �� �������� � ����������!");
            }

            if (romajiTextUI != null)
            {
                romajiTextUI.text = currentData.romaji;
            }
            else
            {
                Debug.LogError("UI ������� ��� ����������� ������� ('romajiTextUI') �� �������� � ����������!");
            }
        }
        else
        {
            Debug.LogError("ScriptableObject 'currentKana' �� �������� � ����������!");
            if (kanaTextUI != null) kanaTextUI.text = "Error!";
            if (romajiTextUI != null) romajiTextUI.text = "No data assigned";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
