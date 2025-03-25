using UnityEngine;

public class KanaCard : CardData
{
    public string kana;
    public string translation;
    public string example;

    public KanaCard(string title, string description, string kana, string translation, string example)
        : base(title, description)
    {
        this.kana = kana;
        this.translation = translation;
        this.example = example;
    }

    public override void PrintCardData()
    {
        base.PrintCardData();
        Debug.Log("Kana: " + kana);
        Debug.Log("Translation: " + translation);
        Debug.Log("Example: " + example);
    }
}
