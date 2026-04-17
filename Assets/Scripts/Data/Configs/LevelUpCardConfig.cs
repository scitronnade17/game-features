using UnityEngine;

[CreateAssetMenu(fileName = "LevelUpCardConfig", menuName = "Configs/LevelUpCards")]
public class LevelUpCardConfig : ScriptableObject
{
    public CardUpgradeId CardId;
    public int Amount;
    public string Label;
    public Sprite Icon;
}