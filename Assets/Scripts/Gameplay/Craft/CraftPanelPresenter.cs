using System;
using UnityEngine;

public interface ICraftPanelPresenter
{
    void Register(CraftPanelUI craftPanelUI);
    void BuildReceiptsList();
}

public class CraftPanelPresenter : ICraftPanelPresenter, IDisposable
{
    private readonly IConfigDataService itemConfigs;
    private readonly ICraftService craftService;
    private readonly ICraftSlotService craftSlotService;
    private readonly ICraftUIService craftUIService;
    private CraftPanelUI craftPanelUI;

    public void Register(CraftPanelUI _craftPanelUI)
    {
        craftPanelUI = _craftPanelUI;
    }

    public CraftPanelPresenter(
      IConfigDataService _itemConfigs,
      ICraftService _craftService,
      ICraftSlotService _craftSlotService,
      ICraftUIService _craftUIService)
    {
        itemConfigs = _itemConfigs;
        craftService = _craftService;
        craftSlotService = _craftSlotService;
        craftUIService = _craftUIService;

        craftService.OnRecipeSelected += OnRecipeSelected;
        craftService.OnSlotsRebuilt += OnSlotsRebuilt;

        craftService.OnSlotsCleared += OnSlotsCleared;
        craftService.OnCraftCompleted += OnCraftCompleted;

        craftSlotService.OnSlotUpdated += OnSlotUpdated;
        craftSlotService.OnAllSlotsUpdated += OnAllSlotsUpdated;
    }

    public void BuildReceiptsList()
    {
        craftUIService.BuildReceiptsList(craftPanelUI.ReceiptRoot);
    }

    private void OnRecipeSelected(CraftRecipeConfig config)
    {
        craftPanelUI.UpdateRecipeHeader(config);
    }

    private void OnSlotsRebuilt()
    {
        craftUIService.RebuildCraftSlotViews(craftService.Slots, craftPanelUI.IngridientRoot);

        foreach (var kv in craftService.Slots)
            UpdateSlot(kv.Value);
    }

    private void OnSlotsCleared()
    {
        craftUIService.ClearCraftSlotViews();
    }

    private void OnCraftCompleted()
    {
        foreach (var kv in craftService.Slots)
            UpdateSlot(kv.Value);
    }

    private void OnSlotUpdated(CraftingSlot slot)
    {
        UpdateSlot(slot);
    }

    private void OnAllSlotsUpdated()
    {
        foreach (var kv in craftService.Slots)
            UpdateSlot(kv.Value);
    }

    private void UpdateSlot(CraftingSlot slot)
    {
        ItemConfig config = itemConfigs.GetItemConfig(slot.ItemId);
        craftUIService.UpdateCraftSlotView(slot.ItemId, config, slot);
    }

    public void Dispose()
    {
        craftService.OnRecipeSelected -= OnRecipeSelected;
        craftService.OnSlotsRebuilt -= OnSlotsRebuilt;

        craftService.OnSlotsCleared -= OnSlotsCleared;
        craftService.OnCraftCompleted -= OnCraftCompleted;

        craftSlotService.OnSlotUpdated -= OnSlotUpdated;
        craftSlotService.OnAllSlotsUpdated -= OnAllSlotsUpdated;
    }
}
