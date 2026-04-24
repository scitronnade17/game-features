using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeCardConfig", menuName = "Configs/UpgradeCards")]
public class UpgradeCardConfig : ScriptableObject
{
    public CardUpgradeId CardId;
    public int Amount;
    public string Label;
    public Sprite Icon;
}