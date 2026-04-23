using UnityEngine;

public class InventoryCellView : MonoBehaviour, IInventoryDropTarget
{
    public InventoryDropTargetType TargetType => InventoryDropTargetType.Inventory;
}