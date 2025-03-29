using UnityEngine;

// Данные, которые мы хотим хранить для каждого символа каны
[CreateAssetMenu(fileName = "New Kana", menuName = "Japanese Learning/Kana Character")]
public class KanaCharacterData : ScriptableObject
{
    [Header("Основные Данные")] // Добавляет заголовок в инспекторе
    public string kanaSymbol; // Сам символ
    public string romaji; // Его чтение латиницей, например "a"

    [Header("Дополнительно")]
    [TextArea] // Делает поле string многострочным в инспекторе
    public string description;

    public Sprite image; // Картинка символa
    public AudioClip pronunciation; // Звук произношения
}
