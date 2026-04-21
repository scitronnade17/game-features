using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class InventoryItemView : MonoBehaviour,
  IBeginDragHandler,
  IDragHandler,
  IEndDragHandler,
  IPointerClickHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private Text countText;

    private RectTransform rect;
    private Vector2 cellSize;
    private Vector2 startPos;
    private float canvasScaleFactor;
    private int count;
    private int itemWidth;
    private int itemHeight;
    private Sprite sprite;
    private Vector2Int rootCell;

    public InventoryItemId Id { get; private set; }

    private IInventoryActionAggregator eventAggregator;

    [Inject]
    public void Construct(IInventoryActionAggregator _eventAggregator)
    {
        eventAggregator = _eventAggregator;
    }

    public void Setup(InventoryItemId _id,
      int _count,
      Sprite _sprite,
      int _itemWidth,
      int _itemHeight,
      Vector2Int _rootCell,
      Vector2 _cellSize,
      float _canvasScaleFactor)
    {
        rootCell = _rootCell;
        sprite = _sprite;
        itemHeight = _itemHeight;
        itemWidth = _itemWidth;
        count = _count;
        Id = _id;

        canvasScaleFactor = _canvasScaleFactor;
        cellSize = _cellSize;
        countText.text = _count.ToString();

        rect = (RectTransform)transform;

        icon.sprite = _sprite;
        icon.enabled = _sprite != null;
        icon.raycastTarget = true;

        var iconRect = (RectTransform)icon.transform;
        iconRect.anchorMin = Vector2.zero;
        iconRect.anchorMax = Vector2.one;
        iconRect.offsetMin = Vector2.zero;
        iconRect.offsetMax = Vector2.zero;

        rect.sizeDelta = new Vector2(
          cellSize.x * _itemWidth,
          cellSize.y * _itemHeight
        );

        rect.anchorMin = rect.anchorMax = new Vector2(0, 1);
        rect.pivot = new Vector2(0, 1);

        SnapToCell(_rootCell);
    }

    private void SnapToCell(Vector2Int cell)
    {
        rect.anchoredPosition = new Vector2(
          cell.x * cellSize.x,
          -cell.y * cellSize.y
        );
    }

    public void SnapBack()
    {
        rect.anchoredPosition = startPos;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        startPos = rect.anchoredPosition;
        transform.SetAsLastSibling();

        eventAggregator.ItemBeginDrag(Id);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvasScaleFactor;
        eventAggregator?.ItemDrag(Id, rect.anchoredPosition, eventData);
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        eventAggregator?.ItemEndDrag(Id, rect.anchoredPosition, eventData);
        rect.anchoredPosition = startPos;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            eventAggregator.InventoryItemRightClick(Id);
    }
}