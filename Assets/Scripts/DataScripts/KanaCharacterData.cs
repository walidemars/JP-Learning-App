using UnityEngine;

// ������, ������� �� ����� ������� ��� ������� ������� ����
[CreateAssetMenu(fileName = "New Kana", menuName = "Japanese Learning/Kana Character")]
public class KanaCharacterData : ScriptableObject
{
    [Header("�������� ������")] // ��������� ��������� � ����������
    public string kanaSymbol; // ��� ������
    public string romaji; // ��� ������ ���������, �������� "a"

    [Header("�������������")]
    [TextArea] // ������ ���� string ������������� � ����������
    public string description;

    public Sprite image; // �������� ������a
    public AudioClip pronunciation; // ���� ������������
}
