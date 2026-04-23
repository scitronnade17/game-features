using UnityEngine;

public readonly struct InventoryResult
{
    public bool IsSuccess => FailReason == InventoryFailReason.None;
    public bool IsFail => FailReason != InventoryFailReason.None;
    public InventoryFailReason FailReason { get; }

    private InventoryResult(InventoryFailReason failReason)
    {
        FailReason = failReason;

        if (IsFail)
            Debug.LogError(ToString());
    }

    public static InventoryResult Success() =>
       new(InventoryFailReason.None);

    public static InventoryResult Fail(InventoryFailReason failReason) =>
       new(failReason);

    public override string ToString() =>
       IsSuccess
          ? "<b><color=#00FF7F>[Inventory]</color></b> Success"
          : $"<b><color=#FF3B30>[Inventory]</color></b> Fail: <b>{FailReason}</b>";
}