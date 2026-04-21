public enum InventoryFailReason
{
    None = 0,
    ItemNotFound = 5,
    OutOfBounds = 6,
    NoFreeSpace = 7,
    InvalidCount = 8,
    ItemConfigNotFound = 9,
    PlaceBlocked = 10,
    StackMergeFailed = 11,
    InvalidConsumeAmount = 12,
    NotEnoughItemsInStack = 13,
    Unknown = 999,
}