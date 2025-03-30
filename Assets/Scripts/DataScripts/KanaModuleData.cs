using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Kana Module", menuName = "Japanese Learning/Kana Module")]
public class KanaModuleData : ScriptableObject
{
    public string moduleName; // Название модуля, напр., "Хирагана 1-5"
    public List<KanaCharacterData> kanaList; // Список символов каны в этом модуле
}
