using System.Collections.Generic;
using UnityEngine.EventSystems;

public interface IInventoryDropTargetResolver
{
    InventoryDropTargetResult Resolve(PointerEventData eventData);
}

public class InventoryDropTargetResolver : IInventoryDropTargetResolver
{
    public InventoryDropTargetResult Resolve(PointerEventData eventData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult hit in results)
        {
            IInventoryDropTarget target = hit.gameObject.GetComponentInParent<IInventoryDropTarget>();
            if (target == null)
                continue;

            return new InventoryDropTargetResult(target);
        }

        return InventoryDropTargetResult.None;
    }
}