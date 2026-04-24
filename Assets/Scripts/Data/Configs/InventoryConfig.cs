using UnityEngine;

[CreateAssetMenu(fileName = "InventoryConfig", menuName = "Configs/Inventory")]
public class InventoryConfig : ScriptableObject
{
    public int Width;
    public int Height;
    public GameObject InventoryViewPrefab;
    public GameObject InventoryCellViewPrefab;
    public GameObject IngridientSlotPrefab;
    public GameObject CraftRecipeViewPrefab;
}