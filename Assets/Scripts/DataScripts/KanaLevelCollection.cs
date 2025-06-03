using UnityEngine;

[CreateAssetMenu(fileName = "HiraganaLevelCollection", menuName = "Kana Data/Level Collection")]
public class KanaLevelCollection : ScriptableObject
{
    public KanaModuleData[] hiraganaLevels;
    public KanaModuleData[] katakanaLevels;
}
