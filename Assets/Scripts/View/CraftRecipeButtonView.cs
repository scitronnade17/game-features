using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class CraftRecipeButtonView : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image icon;
    [SerializeField] private Text title;
    private RecipeId recipeId;

    private ICraftBus craftBus;

    public void Setup(CraftRecipeConfig config, RecipeId _recipeId, ICraftBus bus)
    {
        craftBus = bus;
        recipeId = _recipeId;

        if (icon != null)
        {
            icon.sprite = config.Icon;
            icon.enabled = config.Icon != null;
        }

        if (title != null)
            title.text = config.Name;

        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        craftBus.ReceiptButtonClick(recipeId);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnClick);
    }
}
