using UnityEngine;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "Configs/Items")]
public class ItemConfig : ScriptableObject
{
    public ItemId ItemId;
    public ItemType ItemType;
    public string Name;
    public int MaxStack;
    public int Width;
    public int Height;
    public GameObject Prefab;
    public Sprite Icon;

    public Vector2Int GridSize => new Vector2Int(Width, Height);

    public bool IsStackable()
      => MaxStack > 1;
}
