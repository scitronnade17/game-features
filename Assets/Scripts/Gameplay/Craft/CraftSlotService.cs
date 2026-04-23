using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICraftSlotService
{
    event Action<CraftingSlot> OnSlotUpdated;
    event Action OnAllSlotsUpdated;
    IReadOnlyList<CraftingSlot> GetCraftSlotList();
    void CleanupSlots();
    CraftResult TryPutInSlot(InventoryItem item, ItemId slotId, out int removedCount);
}

public class CraftSlotService : ICraftSlotService
{
    private readonly ICraftService craft;

    public event Action<CraftingSlot> OnSlotUpdated;
    public event Action OnAllSlotsUpdated;
    private List<CraftingSlot> returnSlots = new();

    public CraftSlotService(
       ICraftService _craft)
    {
        craft = _craft;
    }

    public IReadOnlyList<CraftingSlot> GetCraftSlotList()
    {
        returnSlots.Clear();

        foreach (var kv in craft.Slots)
        {
            CraftingSlot slot = kv.Value;

            if (slot == null)
                continue;

            if (slot.CurrentCount <= 0)
                continue;

            returnSlots.Add(slot);
        }

        return returnSlots;
    }

    public void CleanupSlots()
    {
        foreach (CraftingSlot slot in returnSlots)
            slot.Clear();

        returnSlots.Clear();
        OnAllSlotsUpdated?.Invoke();
    }

    public CraftResult TryPutInSlot(InventoryItem item, ItemId slotId, out int removedCount)
    {
        removedCount = 0;

        if (!craft.Slots.TryGetValue(slotId, out CraftingSlot slot))
            return CraftResult.Fail(CraftFailReason.SlotNotFound);

        if (item == null)
            return CraftResult.Fail(CraftFailReason.NullItem);

        if (item.Count <= 0)
            return CraftResult.Fail(CraftFailReason.InvalidItemCount);

        int freeSpace = slot.MaxCount - slot.CurrentCount;
        if (freeSpace <= 0)
            return CraftResult.Fail(CraftFailReason.SlotFull);

        int available = item.Count;
        int putCount = Mathf.Min(available, freeSpace);
        if (putCount <= 0)
            return CraftResult.Fail(CraftFailReason.InvalidItemCount);

        if (!slot.CanAccept(item, putCount))
            return CraftResult.Fail(CraftFailReason.ItemNotAccepted);

        bool isPut = slot.TryPut(item, putCount);
        if (!isPut)
            return CraftResult.Fail(CraftFailReason.PutInSlotFailed);

        removedCount = putCount;

        OnSlotUpdated?.Invoke(slot);
        return CraftResult.Success();
    }

}
