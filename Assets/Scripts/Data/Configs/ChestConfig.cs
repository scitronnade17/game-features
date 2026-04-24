using UnityEngine;

[CreateAssetMenu(fileName = "ChestConfig", menuName = "Configs/Chest")]
public class ChestConfig : ScriptableObject
{
    public string Name;
    public string Id;
    public float DelayTimeAfterOpen;
    public Sprite IconClosed;
    public Sprite IconOpened;
}
