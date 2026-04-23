using UnityEngine;

public readonly struct CraftResult
{
    public bool IsSuccess => FailReason == CraftFailReason.None;
    public bool IsFail => FailReason != CraftFailReason.None;
    public CraftFailReason FailReason { get; }

    private CraftResult(CraftFailReason failReason)
    {
        FailReason = failReason;

        if (IsFail)
            Debug.LogError(ToString());
    }

    public static CraftResult Success() =>
       new(CraftFailReason.None);

    public static CraftResult Fail(CraftFailReason failReason) =>
       new(failReason);

    public override string ToString() =>
       IsSuccess
          ? "<b><color=#00FF7F>[Craft]</color></b> Success"
          : $"<b><color=#FF3B30>[Craft]</color></b> Fail: <b>{FailReason}</b>";
}