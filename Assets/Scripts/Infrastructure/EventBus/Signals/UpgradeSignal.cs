public struct UpgradeSignal
{
    public CardUpgradeId UpgradeId { get; }

    public UpgradeSignal(CardUpgradeId _upgradeId)
    {
        UpgradeId = _upgradeId;
    }
}