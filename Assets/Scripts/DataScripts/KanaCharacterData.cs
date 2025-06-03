using UnityEngine;

[CreateAssetMenu(fileName = "New Kana", menuName = "Japanese Learning/Kana Character")]
public class KanaCharacterData : ScriptableObject
{
    [Header("Основные Данные")]
    public string kanaSymbol;
    public string romaji;

    [Header("Дополнительно")]
    [TextArea]
    public string description;

    public Sprite image;
    public AudioClip pronunciation;
}
