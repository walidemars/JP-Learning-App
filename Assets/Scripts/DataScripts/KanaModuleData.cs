using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Kana Module", menuName = "Japanese Learning/Kana Module")]
public class KanaModuleData : ScriptableObject
{
    public string moduleName; // �������� ������, ����., "�������� 1-5"
    public List<KanaCharacterData> kanaList; // ������ �������� ���� � ���� ������
}
