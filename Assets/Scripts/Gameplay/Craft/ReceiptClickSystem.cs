using System;
using Zenject;

public class ReceiptClickSystem : IInitializable, IDisposable
{
    private readonly IConfigDataService configs;
    private readonly ICraftService craftService;
    private readonly ICraftBus craftBus;

    public ReceiptClickSystem(ICraftBus _craftBus,
      IConfigDataService _configs,
      ICraftService _craftService)
    {
        craftBus = _craftBus;
        configs = _configs;
        craftService = _craftService;
    }

    public void Initialize()
    {
        craftBus.OnReceiptButtonClick += SelectReceipt;
    }

    private void SelectReceipt(RecipeId recipeId)
    {
        var config = configs.GetRecipe(recipeId);

        craftService.SelectRecipe(config);
    }

    public void Dispose()
    {
        craftBus.OnReceiptButtonClick -= SelectReceipt;
    }
}
