using UnityEngine;

[System.Serializable]
public class CardData
{
    public string title;
    public string description;

    public CardData(string title, string description)
    {
        this.title = title;
        this.description = description;
    }

    public virtual void PrintCardData()
    {
        Debug.Log("Title: " + title);
        Debug.Log("Description: " + description);
    }
}
