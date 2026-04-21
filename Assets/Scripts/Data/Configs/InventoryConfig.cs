using UnityEngine;

[CreateAssetMenu(fileName = "InventoryConfig", menuName = "Configs/InventoryConfig")]
public class InventoryConfig : ScriptableObject
{
    public int Width;
    public int Height;
    public GameObject InventoryViewPrefab;
    public GameObject InventoryCellViewPrefab;
}