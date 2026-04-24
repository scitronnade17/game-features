using UnityEngine;

public readonly struct ChestResult
{
    public bool IsSuccess => FailReason == ChestFailReason.None;
    public bool IsFail => FailReason != ChestFailReason.None;
    public ChestFailReason FailReason { get; }

    private ChestResult(ChestFailReason failReason)
    {
        FailReason = failReason;

        if (IsFail)
            Debug.LogError(ToString());
    }

    public static ChestResult Success() =>
       new(ChestFailReason.None);

    public static ChestResult Fail(ChestFailReason failReason) =>
       new(failReason);

    public override string ToString() =>
       IsSuccess
          ? "<b><color=#00FF7F>[Chest]</color></b> Success"
          : $"<b><color=#FF3B30>[Chest]</color></b> Fail: <b>{FailReason}</b>";
}