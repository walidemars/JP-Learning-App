using UnityEngine;


[CreateAssetMenu(fileName = "HiraganaLevelCollection", menuName = "Kana Data/Level Collection")]
public class KanaLevelCollection : ScriptableObject
{
    // ћассив, который будет хранить все уровни хираганы
    public KanaModuleData[] hiraganaLevels;
    // ћассив, который будет хранить все уровни катаканы
    public KanaModuleData[] katakanaLevels;
}
